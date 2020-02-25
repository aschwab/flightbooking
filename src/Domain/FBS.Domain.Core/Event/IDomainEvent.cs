using MediatR;
using System;

namespace FBS.Domain.Core
{
    public interface IDomainEvent : INotification
    {
        Guid EventId { get; }

        Guid AggregateId { get; }

        Type AggregateType { get; }

        long AggregateVersion { get; }

        String GetAggregateName();

        IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion);
    }
}