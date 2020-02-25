using FBS.Domain.Booking;
using FBS.Domain.Booking.Aggregates;
using FBS.Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FBS.Booking.Write.API
{
    public class BookingEventHandler : IEventHandler<BookingRequestedEvent>
    {
        private readonly IMediator mediator;
        private readonly IRepository<FlightAggregate> flightRepository;

        public BookingEventHandler(IMediator mediator, IRepository<FlightAggregate> flightRepository)
        {
            this.mediator = mediator;
            this.flightRepository = flightRepository;
        }

        /// <summary>
        /// Booking has been requested, either Approve or Reject it here
        /// </summary>
        public async Task Handle(BookingRequestedEvent @event, CancellationToken cancellationToken)
        {
            var flight = await flightRepository.GetByIdAsync(@event.FlightId);

            var freeSeat = flight?.Seats?.FirstOrDefault(s => !s.IsOccupied && s.Number == @event.SeatNumber);

            if (freeSeat != null)
            {
                var approveCommand = new ApproveBookingCommand();
                approveCommand.Id = @event.AggregateId;
                approveCommand.FlightId = @event.FlightId;
                approveCommand.SeatNumber = freeSeat.Number;
                await mediator.Publish(approveCommand);
            }
            else
            {
                var rejectCommand = new RejectBookingCommand();
                rejectCommand.Id = @event.AggregateId;
                await mediator.Publish(rejectCommand);
            }
        }
    }
}