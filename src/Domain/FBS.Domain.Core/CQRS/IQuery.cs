using MediatR;

namespace FBS.Domain.Core
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}