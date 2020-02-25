using EventStore.ClientAPI;
using FBS.Domain.Core;
using MediatR;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace FBS.Infrastructure.EventStore
{
    public class EventStoreEventDistributor : IEventDistributor
    {
        private readonly IEventStoreConnection eventStoreConnection;
        private readonly IMediator mediator;
        private readonly EventDistributorSettings settings;
        private EventStoreCatchUpSubscription subscription;

        public EventStoreEventDistributor(Func<IEventStoreConnection> eventStoreConnection, IMediator mediator, EventDistributorSettings settings)
        {
            this.eventStoreConnection = eventStoreConnection();
            this.mediator = mediator;
            this.settings = settings;
        }

        public void Dispose()
        {
            this.subscription.Stop();
            this.eventStoreConnection.Close();
        }

        private void CreateSubscription()
        {
            var subscriptionName = $"{Assembly.GetEntryAssembly().GetName().ToString()}_{Guid.NewGuid().ToString()}";

            //subscribe to all possible events from the event store
            var catchUpSubscriptionSettings = new CatchUpSubscriptionSettings(
                maxLiveQueueSize: 100000,
                readBatchSize: 1000,
                verboseLogging: false,
                resolveLinkTos: false,
                subscriptionName: subscriptionName);

            this.subscription = this.eventStoreConnection.SubscribeToAllFrom(
                lastCheckpoint: settings.StartFromBeginning ? Position.Start : Position.End,
                settings: catchUpSubscriptionSettings,
                eventAppeared: EventAppeared,
                subscriptionDropped: SubscriptionDropped);
        }

        private void SubscriptionDropped(EventStoreCatchUpSubscription obj, SubscriptionDropReason reason, Exception ex)
        {
            throw new EventStoreCommunicationException("Event Store subscription has been dropped, connection lost or buffer full, avoid blocking pipeline with debugging.", ex);
        }

        private async Task EventAppeared(EventStoreCatchUpSubscription sub, ResolvedEvent @event)
        {
            //event store specific events start with $
            if (@event.Event.EventType != null && !@event.Event.EventType.StartsWith("$"))
            {
                var deserializedEvent = EventStoreSerializer.Deserialize(@event.Event.EventType, @event.Event.Data);

                if (deserializedEvent != null)
                {
                    Debug.WriteLine($"Publishing Event Id {deserializedEvent.EventId} of type {@event.Event.EventType}");

                    //dispatch event to internal event pipeline
                    await mediator.Publish(deserializedEvent);
                }
            }

            await Task.CompletedTask;
        }

        public void StartDistributing()
        {
            CreateSubscription();
        }
    }
}