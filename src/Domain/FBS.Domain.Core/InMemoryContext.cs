using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBS.Domain.Core
{
    public class InMemoryContext<TAggregate> : IAggregateContext<TAggregate>
        where TAggregate : class, IAggregate
    {
        private readonly IDictionary<Guid, TAggregate> aggregates;
        private readonly IEventStore eventStore;

        public InMemoryContext(IEventStore eventStore)
        {
            aggregates = new Dictionary<Guid, TAggregate>();
            this.eventStore = eventStore;
        }

        private async Task SaveAsync(TAggregate aggregate)
        {
            aggregates.Add(aggregate.Id, aggregate);
            await Task.CompletedTask;
        }

        public Type AggregateType => typeof(TAggregate);

        public async Task ApplyAsync(IDomainEvent @event)
        {
            var aggregate = aggregates.ContainsKey(@event.AggregateId) ? aggregates[@event.AggregateId] : null;

            if (aggregate == null)
            {
                aggregate = AggregateBase.CreateEmptyAggregate<TAggregate>();
                aggregate.RaiseEvent(@event);
                await SaveAsync(aggregate);
            }
            else
            {
                aggregate.RaiseEvent(@event);
            }
        }

        public void Dispose()
        {
            eventStore.Dispose();
        }

        public async Task<TAggregate> GetAggregateByIdAsync(Guid aggregateId)
        {
            if (aggregates.ContainsKey(aggregateId))
            {
                return await Task.FromResult(aggregates[aggregateId]);
            }

            throw new Exception($"Aggregate {aggregateId} not found in Context");
        }

        public Task<IEnumerable<TAggregate>> GetAllAggregates()
        {
            return Task.FromResult(aggregates.Values.AsEnumerable<TAggregate>());
        }

        public Task<IEnumerable<TAggregate>> GetAggregatesWhere(Func<TAggregate, bool> predicate)
        {
            return Task.FromResult(aggregates.Values.AsEnumerable<TAggregate>().Where(predicate));
        }
    }
}