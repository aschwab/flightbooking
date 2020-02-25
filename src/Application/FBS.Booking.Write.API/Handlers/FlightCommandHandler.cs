using FBS.Domain.Booking.Aggregates;
using FBS.Domain.Booking.Commands;
using FBS.Domain.Booking.Events;
using FBS.Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FBS.Booking.Write.API
{
    public class FlightCommandHandler : ICommandHandler<ReleaseFlightCommand>
    {
        private readonly IRepository<FlightAggregate> flightRepository;

        public FlightCommandHandler(IRepository<FlightAggregate> flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public async Task Handle(ReleaseFlightCommand command, CancellationToken cancellationToken)
        {
            FlightAggregate aggregate = new FlightAggregate();
            FlightReleasedEvent @event = new FlightReleasedEvent(command.Id);

            IList<Seat> seats = new List<Seat>();
            //we assume there are 6 seats per row
            int rows = command.Capacity / 6;

            string[] columns = { "A", "B", "C", "D", "E", "F" };

            for (int row = 1; row <= rows; row++)
            {
                for (int column = 0; column < 6; column++)
                    seats.Add(new Seat()
                    {
                        Row = row,
                        Column = columns[column],
                        IsOccupied = false,
                        Number = $"{row}{columns[column]}"
                    });
            }

            @event.Seats = seats.ToArray();
            @event.Date = command.Date;
            @event.Duration = command.Duration;
            @event.From = command.From;
            @event.To = command.To;
            @event.Gate = command.Gate;
            @event.Number = command.Number;
            @event.PlaneModel = command.PlaneModel;

            aggregate.RaiseEvent(@event);
            await flightRepository.SaveAsync(aggregate);
        }
    }
}