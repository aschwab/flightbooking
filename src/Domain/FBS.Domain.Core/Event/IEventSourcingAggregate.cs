using System.Collections.Generic;

namespace FBS.Domain.Core
{
    internal interface IEventSourcingAggregate
    {
        long Version { get; }

        void ApplyEvent(IDomainEvent @event, long version);

        IEnumerable<IDomainEvent> GetUncommittedEvents();

        void ClearUncommittedEvents();
    }
}