using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FBS.Domain.Core
{
    public interface IAggregateContext : IDisposable
    {
        Type AggregateType { get; }

        Task ApplyAsync(IDomainEvent @event);
    }

    public interface IAggregateContext<TAggregate> : IAggregateContext
        where TAggregate : IAggregate
    {
        Task<TAggregate> GetAggregateByIdAsync(Guid aggregateId);

        Task<IEnumerable<TAggregate>> GetAllAggregates();

        Task<IEnumerable<TAggregate>> GetAggregatesWhere(Func<TAggregate, bool> predicate);
    }
}