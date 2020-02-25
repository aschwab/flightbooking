using FBS.Domain.Booking.Aggregates;
using FBS.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBS.Domain.Booking.Events
{
    public class SeatOccupiedEvent : DomainEventBase<FlightAggregate>
    {
        public SeatOccupiedEvent()
        {
        }

        public string Number { get; set; }
    }
}