using EventStore.ClientAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FBS.Infrastructure.Test
{
    [TestClass]
    public class EventStoreConnectionTest : IDisposable
    {
        private IEventStoreConnection connection;
        private string stream;

        public EventStoreConnectionTest()
        {
            connection = EventStoreConnection.Create(new Uri("tcp://localhost:1113"));
            connection.ConnectAsync().Wait();
            stream = Guid.NewGuid().ToString();
        }

        [TestMethod]
        public async Task TestStreamDoesNotExists()
        {
            var events = await connection.ReadStreamEventsForwardAsync(stream, StreamPosition.Start, 1, false);

            Assert.AreEqual(SliceReadStatus.StreamNotFound, events.Status);
        }

        [TestMethod]
        public async Task TestStreamExists()
        {
            await AppendEventToStreamAsync();

            var events = await connection.ReadStreamEventsForwardAsync(stream, StreamPosition.Start, 1, false);

            Assert.AreEqual(SliceReadStatus.Success, events.Status);
            Assert.AreEqual(events.Events.Length, 1);
        }

        [TestMethod]
        public async Task TestPerformance()
        {
            for (int i = 0; i < 100; i++)
            {
                await connection.AppendToStreamAsync(stream, i - 1,
                    new EventData(Guid.NewGuid(), "test", true, Encoding.UTF8.GetBytes("{}"), StreamMetadata.Create().AsJsonBytes()));
            }
        }

        private async Task AppendEventToStreamAsync()
        {
            await connection.AppendToStreamAsync(stream, ExpectedVersion.NoStream,
                new EventData(Guid.NewGuid(), "test", true, Encoding.UTF8.GetBytes("{}"), StreamMetadata.Create().AsJsonBytes()));
        }

        public void Dispose()
        {
            connection.DeleteStreamAsync(stream, ExpectedVersion.Any).Wait();
            connection.Dispose();
        }
    }
}