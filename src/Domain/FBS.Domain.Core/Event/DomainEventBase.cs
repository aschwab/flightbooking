using System;
using System.Collections.Generic;

namespace FBS.Domain.Core
{
    public abstract class DomainEventBase : IDomainEvent, IEquatable<DomainEventBase>
    {
        protected DomainEventBase()
        {
            EventId = Guid.NewGuid();
        }

        protected DomainEventBase(Guid aggregateId) : this()
        {
            AggregateId = aggregateId;
        }

        protected DomainEventBase(Guid aggregateId, long aggregateVersion) : this(aggregateId)
        {
            AggregateVersion = aggregateVersion;
        }

        public Guid EventId { get; private set; }

        public Guid AggregateId { get; set; }

        public long AggregateVersion { get; set; }

        public abstract Type AggregateType { get; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as DomainEventBase);
        }

        public bool Equals(DomainEventBase other)
        {
            return other != null &&
                   EventId.Equals(other.EventId);
        }

        public override int GetHashCode()
        {
            return 290933282 + EqualityComparer<Guid>.Default.GetHashCode(EventId);
        }

        public virtual IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            this.AggregateId = aggregateId;
            this.AggregateVersion = aggregateVersion;
            return this;
        }

        public abstract String GetAggregateName();
    }

    public abstract class DomainEventBase<TAggregate> : DomainEventBase
        where TAggregate : AggregateBase
    {
        protected DomainEventBase()
        {
        }

        protected DomainEventBase(Guid aggregateId) : base(aggregateId)
        {
        }

        protected DomainEventBase(Guid aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion)
        {
        }

        public override Type AggregateType => typeof(TAggregate);

        public override string GetAggregateName()
        {
            return typeof(TAggregate).Name + "-" + AggregateId.ToString();
        }
    }
}