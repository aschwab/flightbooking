using FBS.Domain.Booking.Aggregates;
using FBS.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBS.Domain.Booking.Commands
{
    public class ReleaseFlightCommand : ICommand
    {
        public Guid Id { get; set; }

        public int Capacity { get; set; }

        public Location From { get; set; }

        public Location To { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Duration { get; set; }

        public string Gate { get; set; }

        public string Number { get; set; }

        public string PlaneModel { get; set; }
    }
}