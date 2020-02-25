using MediatR;

namespace FBS.Domain.Core
{
    public interface IEventHandler<TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
    {
    }
}