using System;
using System.Text;
using FBS.Domain.Core;
using Newtonsoft.Json;

namespace FBS.Infrastructure.EventStore
{
    public static class EventStoreSerializer
    {
        public static IDomainEvent Deserialize(string eventType, byte[] data)
        {
            return Deserialize(Type.GetType(eventType), data);
        }

        public static IDomainEvent Deserialize(Type eventType, byte[] data)
        {
            if (eventType != null)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() };
                return (IDomainEvent)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), eventType, settings);
            }

            return null;
        }

        public static byte[] Serialize(IDomainEvent @event)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
        }
    }
}