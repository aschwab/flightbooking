using System;

namespace FBS.Domain.Core
{
    public interface IEventDistributor : IDisposable
    {
        void StartDistributing();
    }
}