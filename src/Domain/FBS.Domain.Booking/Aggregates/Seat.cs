using System;
using System.Collections.Generic;
using System.Text;

namespace FBS.Domain.Booking.Aggregates
{
    public class Seat
    {
        public string Number { get; set; }

        public int Row { get; set; }

        public string Column { get; set; }

        public bool IsOccupied { get; set; }
    }
}