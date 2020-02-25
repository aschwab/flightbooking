using FBS.Domain.Core;
using System;

namespace FBS.Domain.Booking
{
    public class RejectBookingCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}