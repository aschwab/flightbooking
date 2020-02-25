using FBS.Domain.Core;
using System;

namespace FBS.Domain.Booking
{
    public class BookingAggregate : AggregateBase
    {
        public BookingAggregate()
        {
        }

        public Guid FlightId { get; set; }

        public Guid CustomerId { get; set; }

        public string BookingNumber { get; set; }

        public string SeatNumber { get; set; }

        public BookingState State { get; set; }

        /// <summary>
        /// Booking has been requested so this aggregate has been instantiated
        /// </summary>
        public void Apply(BookingRequestedEvent @event)
        {
            this.FlightId = @event.FlightId;
            this.Id = @event.AggregateId;
            this.CustomerId = @event.CustomerId;
            this.SeatNumber = @event.SeatNumber;
        }

        /// <summary>
        /// Booking has been approved
        /// </summary>
        public void Apply(BookingApprovedEvent @event)
        {
            this.State = BookingState.Approved;
            this.BookingNumber = @event.BookingNumber;
        }

        /// <summary>
        /// Booking has been rejected
        /// </summary>
        /// <param name="event"></param>
        public void Apply(BookingRejectedEvent @event)
        {
            this.State = BookingState.Rejected;
        }
    }
}