using FBS.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBS.Domain.Booking
{
    public class BookingRejectedEvent : DomainEventBase<BookingAggregate>
    {
        public BookingRejectedEvent()
        {
        }

        public BookingRejectedEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}