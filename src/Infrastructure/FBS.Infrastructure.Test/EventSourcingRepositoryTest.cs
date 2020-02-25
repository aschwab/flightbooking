using EventStore.ClientAPI;
using FBS.Domain.Core;
using FBS.Infrastructure.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace FBS.Infrastructure.Test
{
    [TestClass]
    public class EventSourcingRepositoryTest : IDisposable
    {
        private IEventStoreConnection connection;
        private IEventStore store;
        private IRepository<TestAggregate> testRepository;

        public EventSourcingRepositoryTest()
        {
            connection = EventStoreConnection.Create(new Uri("tcp://localhost:1113"));
            connection.ConnectAsync().Wait();

            store = new EventStoreEventStore(() => connection);
            testRepository = new EventSourcingRepository<TestAggregate>(store);
        }

        [TestMethod]
        public async Task TestAddEvent()
        {
            TestAggregate aggregate = new TestAggregate(Guid.NewGuid());
            aggregate.AddTestEvent("Test");

            await testRepository.SaveAsync(aggregate);
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}