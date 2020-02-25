using FBS.Domain.Booking;
using FBS.Domain.Booking.Aggregates;
using FBS.Domain.Booking.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FBS.Booking.Read.API
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator mediator;

        public BookingController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(nameof(GetBookingsByFlightQuery))]
        public async Task<IEnumerable<BookingAggregate>> Get([FromQuery] GetBookingsByFlightQuery request)
        {
            return await mediator.Send(request);
        }

        [HttpGet(nameof(GetFlightByIdQuery))]
        public async Task<FlightAggregate> Get([FromQuery] GetFlightByIdQuery request)
        {
            return await mediator.Send(request);
        }

        [HttpGet(nameof(GetAllFlightsQuery))]
        public async Task<IEnumerable<FlightAggregate>> Get([FromQuery] GetAllFlightsQuery request)
        {
            return await mediator.Send(request);
        }
    }
}