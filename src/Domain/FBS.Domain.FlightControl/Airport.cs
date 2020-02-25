using System;
using System.Collections.Generic;
using System.Text;

namespace FBS.Domain.FlightControl
{
    public class Airport
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}