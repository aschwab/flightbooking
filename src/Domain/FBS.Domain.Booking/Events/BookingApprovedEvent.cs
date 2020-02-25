using FBS.Domain.Core;
using System;

namespace FBS.Domain.Booking
{
    public class BookingApprovedEvent : DomainEventBase<BookingAggregate>
    {
        public BookingApprovedEvent()
        {
        }

        public BookingApprovedEvent(Guid aggregateId) : base(aggregateId)
        {
        }

        public string BookingNumber { get; set; }
    }
}