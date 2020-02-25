using FBS.Domain.Booking;
using FBS.Domain.Booking.Aggregates;
using FBS.Domain.Booking.Events;
using FBS.Domain.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FBS.Booking.Write.API
{
    public class BookingCommandHandler : ICommandHandler<BookFlightCommand>,
        ICommandHandler<ApproveBookingCommand>,
        ICommandHandler<RejectBookingCommand>
    {
        private readonly IRepository<BookingAggregate> bookingRepository;
        private readonly IRepository<FlightAggregate> flightRepository;

        public BookingCommandHandler(IRepository<BookingAggregate> bookingRepository, IRepository<FlightAggregate> flightRepository)
        {
            this.bookingRepository = bookingRepository;
            this.flightRepository = flightRepository;
        }

        public async Task Handle(BookFlightCommand notification, CancellationToken cancellationToken)
        {
            BookingAggregate aggregate = new BookingAggregate();
            BookingRequestedEvent @event = new BookingRequestedEvent(Guid.NewGuid()); //generate aggregate id here
            @event.FlightId = notification.Id;
            @event.CustomerId = notification.CustomerId;
            @event.SeatNumber = notification.SeatNumber;
            aggregate.RaiseEvent(@event);
            await bookingRepository.SaveAsync(aggregate);
        }

        public async Task Handle(ApproveBookingCommand notification, CancellationToken cancellationToken)
        {
            //occupy the seat
            var flightAggregate = await flightRepository.GetByIdAsync(notification.FlightId);
            var seatOccupiedEvent = new SeatOccupiedEvent();
            seatOccupiedEvent.Number = notification.SeatNumber;
            flightAggregate.RaiseEvent(seatOccupiedEvent);
            await flightRepository.SaveAsync(flightAggregate);

            //approve the booking
            var bookingAggregate = await bookingRepository.GetByIdAsync(notification.Id);
            var bookingApprovedEvent = new BookingApprovedEvent(notification.Id);
            bookingApprovedEvent.BookingNumber = $"{flightAggregate.Number}_{notification.SeatNumber}";
            bookingAggregate.RaiseEvent(bookingApprovedEvent);
            await bookingRepository.SaveAsync(bookingAggregate);
        }

        public async Task Handle(RejectBookingCommand notification, CancellationToken cancellationToken)
        {
            var bookingAggregate = await bookingRepository.GetByIdAsync(notification.Id);
            var bookingRejectedEvent = new BookingRejectedEvent(notification.Id);
            bookingAggregate.RaiseEvent(bookingRejectedEvent);
            await bookingRepository.SaveAsync(bookingAggregate);
        }
    }
}