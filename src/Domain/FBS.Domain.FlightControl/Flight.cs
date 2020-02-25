using System;

namespace FBS.Domain.FlightControl
{
    public class Flight
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Duration { get; set; }

        public Airport From { get; set; }

        public Airport To { get; set; }

        public string Gate { get; set; }

        public Plane Plane { get; set; }

        public string Number { get; set; }
    }
}