using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FBS.Domain.Core
{
    public interface IEventStore : IDisposable
    {
        Task<IEnumerable<Event>> ReadEventsAsync(String id);

        Task<IEnumerable<Event>> ReadAllEventsAsync();

        Task AppendEventAsync(IDomainEvent @event);
    }
}