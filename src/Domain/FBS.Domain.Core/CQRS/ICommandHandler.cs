using MediatR;

namespace FBS.Domain.Core
{
    public interface ICommandHandler<TCommand> : INotificationHandler<TCommand>
        where TCommand : ICommand
    {
    }
}