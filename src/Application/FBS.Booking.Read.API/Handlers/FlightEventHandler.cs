using FBS.Domain.Booking;
using FBS.Domain.Booking.Aggregates;
using FBS.Domain.Booking.Events;
using FBS.Domain.Core;
using System.Threading;
using System.Threading.Tasks;

namespace FBS.Booking.Read.API
{
    public class FlightEventHandler : IEventHandler<FlightReleasedEvent>,
        IEventHandler<SeatOccupiedEvent>
    {
        private readonly IAggregateContext<FlightAggregate> flightContext;

        public FlightEventHandler(IAggregateContext<FlightAggregate> flightContext)
        {
            this.flightContext = flightContext;
        }

        public async Task Handle(FlightReleasedEvent notification, CancellationToken cancellationToken)
        {
            await flightContext.ApplyAsync(notification);
        }

        public async Task Handle(SeatOccupiedEvent notification, CancellationToken cancellationToken)
        {
            await flightContext.ApplyAsync(notification);
        }
    }
}