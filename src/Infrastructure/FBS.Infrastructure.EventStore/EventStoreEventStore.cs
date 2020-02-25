using EventStore.ClientAPI;
using EventStore.ClientAPI.Exceptions;
using FBS.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBS.Infrastructure.EventStore
{
    public class EventStoreEventStore : IEventStore
    {
        private readonly IEventStoreConnection connection;

        public EventStoreEventStore(Func<IEventStoreConnection> connection)
        {
            this.connection = connection();
        }

        public void Dispose()
        {
            this.connection.Close();
        }

        public async Task<IEnumerable<Event>> ReadEventsAsync(String id)
        {
            try
            {
                var events = new List<Event>();
                StreamEventsSlice currentSlice;
                long nextSliceStart = StreamPosition.Start;

                do
                {
                    currentSlice = await connection.ReadStreamEventsForwardAsync(id, nextSliceStart, 200, false);
                    if (currentSlice.Status != SliceReadStatus.Success)
                    {
                        throw new EventStoreAggregateNotFoundException($"Aggregate {id} not found");
                    }
                    nextSliceStart = currentSlice.NextEventNumber;
                    foreach (var resolvedEvent in currentSlice.Events)
                    {
                        events.Add(new Event(EventStoreSerializer.Deserialize(resolvedEvent.Event.EventType, resolvedEvent.Event.Data), resolvedEvent.Event.EventNumber));
                    }
                } while (!currentSlice.IsEndOfStream);

                return events;
            }
            catch (EventStoreConnectionException ex)
            {
                throw new EventStoreCommunicationException($"Error while reading events for aggregate {id}", ex);
            }
        }

        public async Task<IEnumerable<Event>> ReadAllEventsAsync()
        {
            try
            {
                var events = new List<Event>();
                AllEventsSlice currentSlice;
                var nextSliceStart = Position.Start;

                do
                {
                    currentSlice = await connection.ReadAllEventsForwardAsync(nextSliceStart, 200, false);
                    nextSliceStart = currentSlice.NextPosition;

                    foreach (var resolvedEvent in currentSlice.Events.Where(e => !e.Event.EventType.StartsWith("$")))
                    {
                        events.Add(new Event(EventStoreSerializer.Deserialize(resolvedEvent.Event.EventType, resolvedEvent.Event.Data), resolvedEvent.Event.EventNumber));
                    }
                } while (!currentSlice.IsEndOfStream);

                return events;
            }
            catch (EventStoreConnectionException ex)
            {
                throw new EventStoreCommunicationException($"Error while reading all events", ex);
            }
        }

        public async Task AppendEventAsync(IDomainEvent @event)
        {
            try
            {
                if (@event.AggregateId == Guid.Empty || @event.EventId == Guid.Empty)
                {
                    throw new ArgumentException("AggregateId and EventId can't be null or empty when applying to event stream.");
                }

                var eventData = new EventData(
                    @event.EventId,
                    @event.GetType().AssemblyQualifiedName,
                    true,
                    EventStoreSerializer.Serialize(@event),
                    Encoding.UTF8.GetBytes("{}"));

                Console.WriteLine($"Applying Event with Id {@event.EventId} of type {@event.GetType().AssemblyQualifiedName}");
                await connection.AppendToStreamAsync(
                    @event.GetAggregateName(),
                    @event.AggregateVersion == AggregateBase.NewAggregateVersion ? ExpectedVersion.NoStream : @event.AggregateVersion,
                    eventData);
            }
            catch (EventStoreConnectionException ex)
            {
                throw new EventStoreCommunicationException($"Error while appending event {@event.EventId} for aggregate {@event.AggregateId}", ex);
            }
        }
    }
}