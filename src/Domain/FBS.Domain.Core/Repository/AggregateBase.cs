using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FBS.Domain.Core
{
    public abstract class AggregateBase : IAggregate, IEventSourcingAggregate
    {
        public const long NewAggregateVersion = -1;

        private readonly ICollection<IDomainEvent> uncommittedEvents = new LinkedList<IDomainEvent>();
        private long version = NewAggregateVersion;

        public Guid Id { get; protected set; }

        long IEventSourcingAggregate.Version => version;

        void IEventSourcingAggregate.ApplyEvent(IDomainEvent @event, long version)
        {
            if (!uncommittedEvents.Any(x => Equals(x.EventId, @event.EventId)))
            {
                ((dynamic)this).Apply((dynamic)@event);
                this.version = version;
            }
        }

        void IEventSourcingAggregate.ClearUncommittedEvents()
        {
            uncommittedEvents.Clear();
        }

        IEnumerable<IDomainEvent> IEventSourcingAggregate.GetUncommittedEvents()
        {
            return uncommittedEvents.AsEnumerable();
        }

        public void RaiseEvent<TEvent>(TEvent @event)
            where TEvent : IDomainEvent
        {
            IDomainEvent eventWithAggregate = @event.WithAggregate(
                Equals(Id, default(Guid)) ? @event.AggregateId : Id,
                version);

            ((IEventSourcingAggregate)this).ApplyEvent(eventWithAggregate, version + 1);
            uncommittedEvents.Add(eventWithAggregate);
        }

        public static TAggregate CreateEmptyAggregate<TAggregate>()
        {
            return (TAggregate)typeof(TAggregate)
                    .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                        null, new Type[0], new ParameterModifier[0])
                    .Invoke(new object[0]);
        }
    }
}