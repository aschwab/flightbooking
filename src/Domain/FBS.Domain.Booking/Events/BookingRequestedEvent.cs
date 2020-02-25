using FBS.Domain.Core;
using System;

namespace FBS.Domain.Booking
{
    public class BookingRequestedEvent : DomainEventBase<BookingAggregate>
    {
        public BookingRequestedEvent()
        {
        }

        public BookingRequestedEvent(Guid aggregateId) : base(aggregateId)
        {
        }

        public Guid FlightId { get; set; }

        public Guid CustomerId { get; set; }

        public string SeatNumber { get; set; }
    }
}