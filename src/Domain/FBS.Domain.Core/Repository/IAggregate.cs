using System;

namespace FBS.Domain.Core
{
    public interface IAggregate
    {
        Guid Id { get; }

        public void RaiseEvent<TEvent>(TEvent @event)
            where TEvent : IDomainEvent;
    }
}