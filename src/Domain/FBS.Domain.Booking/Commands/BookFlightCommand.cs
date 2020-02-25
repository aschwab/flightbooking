using FBS.Domain.Core;
using System;

namespace FBS.Domain.Booking
{
    public class BookFlightCommand : ICommand
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string SeatNumber { get; set; }
    }
}