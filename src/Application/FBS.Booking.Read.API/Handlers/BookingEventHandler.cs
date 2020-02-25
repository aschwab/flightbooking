using FBS.Domain.Booking;
using FBS.Domain.Core;
using System.Threading;
using System.Threading.Tasks;

namespace FBS.Booking.Read.API
{
    public class BookingEventHandler : IEventHandler<BookingRequestedEvent>,
        IEventHandler<BookingApprovedEvent>,
        IEventHandler<BookingRejectedEvent>
    {
        private readonly IAggregateContext<BookingAggregate> bookingContext;

        public BookingEventHandler(IAggregateContext<BookingAggregate> bookingContext)
        {
            this.bookingContext = bookingContext;
        }

        public async Task Handle(BookingRequestedEvent notification, CancellationToken cancellationToken)
        {
            await bookingContext.ApplyAsync(notification);
        }

        public async Task Handle(BookingApprovedEvent notification, CancellationToken cancellationToken)
        {
            await bookingContext.ApplyAsync(notification);
        }

        public async Task Handle(BookingRejectedEvent notification, CancellationToken cancellationToken)
        {
            await bookingContext.ApplyAsync(notification);
        }
    }
}