using System;
using System.Threading.Tasks;

namespace FBS.Domain.Core
{
    public interface IRepository<TAggregate>
        where TAggregate : AggregateBase, IAggregate
    {
        Task<TAggregate> GetByIdAsync(Guid id);

        Task SaveAsync(TAggregate aggregate);
    }

    [Serializable]
    public class RepositoryException : Exception
    {
        public RepositoryException()
        {
        }

        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RepositoryException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}