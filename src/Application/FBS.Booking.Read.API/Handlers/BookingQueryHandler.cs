using FBS.Domain.Booking;
using FBS.Domain.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FBS.Booking.Read.API
{
    public class BookingQueryHandler : IQueryHandler<GetBookingsByFlightQuery, IEnumerable<BookingAggregate>>
    {
        private readonly IAggregateContext<BookingAggregate> bookingContext;

        public BookingQueryHandler(IAggregateContext<BookingAggregate> bookingContext)
        {
            this.bookingContext = bookingContext;
        }

        public async Task<IEnumerable<BookingAggregate>> Handle(GetBookingsByFlightQuery request, CancellationToken cancellationToken)
        {
            return await bookingContext.GetAggregatesWhere(booking => booking.FlightId == request.FlightId
                && booking.CustomerId == request.CustomerId);
        }
    }
}