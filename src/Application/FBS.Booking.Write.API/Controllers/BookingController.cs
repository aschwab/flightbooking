using FBS.Domain.Booking;
using FBS.Domain.Booking.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FBS.Booking.Write.API
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

        [HttpPost(nameof(BookFlightCommand))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] BookFlightCommand command)
        {
            if (command.CustomerId != Guid.Empty && command.Id != Guid.Empty
                && !String.IsNullOrEmpty(command.SeatNumber))
            {
                await mediator.Publish(command);
                return Ok();
            }
            else
            {
                return Problem(detail: "Error validating the BookFlightCommand", statusCode: 400);
            }
        }

        [HttpPost(nameof(ReleaseFlightCommand))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ReleaseFlightCommand command)
        {
            if (command.Id != Guid.Empty
                && !String.IsNullOrEmpty(command.Gate)
                && !String.IsNullOrEmpty(command.Number)
                && !String.IsNullOrEmpty(command.PlaneModel)
                && command.Capacity > 0
                && command.Date != DateTime.MinValue
                && command.Duration != TimeSpan.MinValue
                && command.From != null
                && command.To != null)
            {
                await mediator.Publish(command);
                return Ok();
            }
            else
            {
                return Problem(detail: "Error validating the ReleaseFlightCommand", statusCode: 400);
            }
        }
    }
}