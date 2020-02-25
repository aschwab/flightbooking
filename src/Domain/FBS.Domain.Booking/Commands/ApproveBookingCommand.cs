using FBS.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBS.Domain.Booking
{
    public class ApproveBookingCommand : ICommand
    {
        public Guid Id { get; set; }

        public Guid FlightId { get; set; }

        public string SeatNumber { get; set; }
    }
}