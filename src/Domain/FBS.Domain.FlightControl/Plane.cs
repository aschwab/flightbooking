using System;
using System.Collections.Generic;
using System.Text;

namespace FBS.Domain.FlightControl
{
    public class Plane
    {
        public Guid Id { get; set; }

        public int Capacity { get; set; }

        public string Model { get; set; }
    }
}