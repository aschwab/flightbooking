using FBS.Domain.Core;
using System;

namespace FBS.Infrastructure.Test
{
    public class TestAggregateCreatedEvent : DomainEventBase<TestAggregate>
    {
        public TestAggregateCreatedEvent(Guid aggregateId) : base(aggregateId)
        {
        }

        public TestAggregateCreatedEvent(Guid aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion)
        {
        }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new TestAggregateCreatedEvent(aggregateId, aggregateVersion);
        }
    }
}