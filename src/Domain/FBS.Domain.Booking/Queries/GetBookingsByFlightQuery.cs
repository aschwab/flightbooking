using FBS.Domain.Core;
using System;
using System.Collections.Generic;

namespace FBS.Domain.Booking
{
    public class GetBookingsByFlightQuery : IQuery<IEnumerable<BookingAggregate>>
    {
        public Guid FlightId { get; set; }

        public Guid CustomerId { get; set; }
    }
}