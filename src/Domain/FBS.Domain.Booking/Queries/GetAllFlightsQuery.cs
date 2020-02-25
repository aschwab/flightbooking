using FBS.Domain.Booking.Aggregates;
using FBS.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBS.Domain.Booking.Queries
{
    public class GetAllFlightsQuery : IQuery<IEnumerable<FlightAggregate>>
    {
    }
}