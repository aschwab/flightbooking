using FBS.Domain.Booking.Aggregates;
using FBS.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBS.Domain.Booking.Queries
{
    public class GetFlightByIdQuery : IQuery<FlightAggregate>
    {
        public Guid Id { get; set; }
    }
}