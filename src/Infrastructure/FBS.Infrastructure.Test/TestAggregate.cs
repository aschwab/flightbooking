using FBS.Domain.Core;
using System;

namespace FBS.Infrastructure.Test
{
    public class TestAggregate : AggregateBase
    {
        public TestAggregate(Guid testId)
        {
            RaiseEvent(new TestAggregateCreatedEvent(testId));
        }

        public string TestString { get; set; }

        public void AddTestEvent(string testString)
        {
            RaiseEvent(new TestEvent(this.Id)
            {
                TestString = testString
            });
        }

        public void Apply(TestAggregateCreatedEvent @event)
        {
            this.Id = @event.AggregateId;
        }

        public void Apply(TestEvent @event)
        {
            this.TestString = @event.TestString;
        }
    }
}