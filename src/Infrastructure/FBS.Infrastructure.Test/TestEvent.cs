using FBS.Domain.Core;
using System;

namespace FBS.Infrastructure.Test
{
    public class TestEvent : DomainEventBase<TestAggregate>
    {
        public TestEvent(Guid aggregateId) : base(aggregateId)
        {
        }

        public TestEvent(Guid aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion)
        {
        }

        public string TestString { get; set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new TestEvent(aggregateId, aggregateVersion);
        }
    }
}