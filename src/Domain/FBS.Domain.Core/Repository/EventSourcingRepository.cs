using System;
using System.Threading.Tasks;

namespace FBS.Domain.Core
{
    public class EventSourcingRepository<TAggregate> : IRepository<TAggregate>
            where TAggregate : AggregateBase, IAggregate
    {
        private readonly IEventStore eventStore;

        public EventSourcingRepository(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public async Task<TAggregate> GetByIdAsync(Guid id)
        {
            try
            {
                var aggregate = AggregateBase.CreateEmptyAggregate<TAggregate>();
                string aggregateId = typeof(TAggregate).Name + "-" + id;
                IEventSourcingAggregate aggregatePersistence = aggregate;

                foreach (var @event in await eventStore.ReadEventsAsync(aggregateId))
                {
                    aggregatePersistence.ApplyEvent(@event.DomainEvent, @event.EventNumber);
                }

                return aggregate;
            }
            catch (EventStoreAggregateNotFoundException)
            {
                return null;
            }
            catch (EventStoreCommunicationException ex)
            {
                throw new RepositoryException("Unable to access event store.", ex);
            }
        }

        public async Task SaveAsync(TAggregate aggregate)
        {
            try
            {
                IEventSourcingAggregate aggregatePersistence = aggregate;

                foreach (var @event in aggregatePersistence.GetUncommittedEvents())
                {
                    await eventStore.AppendEventAsync(@event);
                }
                aggregatePersistence.ClearUncommittedEvents();
            }
            catch (EventStoreCommunicationException ex)
            {
                throw new RepositoryException("Unable to access event store.", ex);
            }
        }
    }
}