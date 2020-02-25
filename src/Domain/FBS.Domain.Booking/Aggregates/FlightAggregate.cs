using FBS.Domain.Booking.Events;
using FBS.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Domain.Booking.Aggregates
{
    public class FlightAggregate : AggregateBase
    {
        public FlightAggregate()
        {
        }

        public Seat[] Seats { get; set; }

        public Location From { get; set; }

        public Location To { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime Arrival => Date.Add(Duration);

        public string Number { get; set; }

        public string Gate { get; set; }

        public string PlaneModel { get; set; }

        /// <summary>
        /// Flight has been released by the FlightControl
        /// </summary>
        /// <param name="event"></param>
        public void Apply(FlightReleasedEvent @event)
        {
            this.Id = @event.AggregateId;
            this.Seats = @event.Seats;
            this.From = @event.From;
            this.To = @event.To;
            this.Date = @event.Date;
            this.Duration = @event.Duration;
            this.Gate = @event.Gate;
            this.Number = @event.Number;
            this.PlaneModel = @event.PlaneModel;
        }

        /// <summary>
        /// A booking was successfull and the seat is now occupied
        /// </summary>
        /// <param name="event"></param>
        public void Apply(SeatOccupiedEvent @event)
        {
            var seat = this.Seats?.FirstOrDefault(s => s.Number == @event.Number);
            seat.IsOccupied = true;
        }
    }
}