using FBS.Domain.Booking;
using FBS.Domain.Booking.Aggregates;
using FBS.Domain.Booking.Queries;
using FBS.Domain.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FBS.Booking.Read.API
{
    public class FlightQueryHandler : IQueryHandler<GetFlightByIdQuery, FlightAggregate>,
        IQueryHandler<GetAllFlightsQuery, IEnumerable<FlightAggregate>>
    {
        private readonly IAggregateContext<FlightAggregate> flightContext;

        public FlightQueryHandler(IAggregateContext<FlightAggregate> flightContext)
        {
            this.flightContext = flightContext;
        }

        public async Task<FlightAggregate> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
        {
            return await flightContext.GetAggregateByIdAsync(request.Id);
        }

        public async Task<IEnumerable<FlightAggregate>> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
        {
            return await flightContext.GetAllAggregates();
        }
    }
}