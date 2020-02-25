using FBS.Domain.FlightControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBS.FlightControl.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private IEnumerable<Flight> flights;
        private IEnumerable<Airport> airports;
        private IEnumerable<Plane> planes;

        public FlightController()
        {
            this.planes = new Plane[]
            {
                new Plane()
                {
                    Id = Guid.Parse("{4180f157-2e96-4114-a218-b4ae7fbbfc73}"),
                    Capacity = 10,
                    Model = "Airbus A320"
                },
                new Plane()
                {
                    Id = Guid.Parse("{5bcc81d2-1c0c-49aa-83f0-c2a3501cd9e2}"),
                    Capacity = 400,
                    Model = "Lockheed L-1011 TriStar"
                },
                new Plane()
                {
                    Id = Guid.Parse("{43e2bb88-f928-4cc6-a183-b381acba27f7}"),
                    Capacity = 853,
                    Model = "Airbus A380"
                },
                new Plane()
                {
                    Id = Guid.Parse("{376735c4-9893-4a38-82c6-8a9f6afb30d0}"),
                    Capacity = 409,
                    Model = "Boing 767-400"
                },
                new Plane()
                {
                    Id = Guid.Parse("{6d42d21a-a072-4794-97f1-d27648094a62}"),
                    Capacity = 410,
                    Model = "McDonnell Douglas MD-11"
                },
                new Plane()
                {
                    Id = Guid.Parse("{6b430e25-8c6f-47cd-bcc8-e64d693f6211}"),
                    Capacity = 436,
                    Model = "Iljuschin Il-96-400M"
                }
            };

            this.airports = new Airport[]
            {
                new Airport()
                {
                    Id = Guid.Parse("{a7aebbba-7a4b-437b-b3cb-9a451b32903f}"),
                    Name = "Vienna, Austria",
                    Code = "VIE"
                },
                new Airport()
                {
                    Id = Guid.Parse("{73c00722-0a29-4ce0-889f-4007d9081976}"),
                    Name = "Linz, Austria",
                    Code = "LNZ"
                },
                new Airport()
                {
                    Id = Guid.Parse("{d6bcfcbc-4a8d-4dec-80fe-8b1f6c60eed6}"),
                    Name = "Las Vegas, United States of America",
                    Code = "LAS"
                },
                new Airport()
                {
                    Id = Guid.Parse("{6b9a9624-b7d9-4864-9b68-2f4261ba9024}"),
                    Name = "London, United Kingdom",
                    Code = "LON"
                },
                new Airport()
                {
                    Id = Guid.Parse("{92c50017-e678-4082-a6ed-e81fa9c23e9e}"),
                    Name = "Düsseldorf, Germany",
                    Code = "DUS"
                },
                new Airport()
                {
                    Id = Guid.Parse("{2386402a-2f48-46d9-9ae2-61f2232b1344}"),
                    Name = "München, Germany",
                    Code = "MUC"
                },
                new Airport()
                {
                    Id = Guid.Parse("{0bde539a-e457-4413-9ea2-ef8ffff2f50e}"),
                    Name = "Rom, Italia",
                    Code = "ROM"
                },
                new Airport()
                {
                    Id = Guid.Parse("{8aedb7c4-5073-4762-9764-ac39e06ca9f1}"),
                    Name = "Madrid, Spain",
                    Code = "MAD"
                },
                new Airport()
                {
                    Id = Guid.Parse("{f5cfb695-a8ab-4d2d-b3f0-e06328b14644}"),
                    Name = "Barcelona, Spain",
                    Code = "BCN"
                },
                new Airport()
                {
                    Id = Guid.Parse("{785b8cf9-b186-41df-b4af-af2dd9ab3542}"),
                    Name = "Tel Aviv, Israel",
                    Code = "TLV"
                },
                new Airport()
                {
                    Id = Guid.Parse("{c16c2ced-0e25-4189-b705-ab86a5cae39b}"),
                    Name = "Bankok, Philipinen",
                    Code = "BKK"
                },
                new Airport()
                {
                    Id = Guid.Parse("{7ee85484-9c8e-491a-a28e-04c6a8bea1ac}"),
                    Name = "Frankfurt am Main, Germany",
                    Code = "FRA"
                }
            };

            this.flights = new Flight[]
            {
                new Flight()
                {
                    Id = Guid.Parse("{1a26b0e2-03a1-4cb3-b393-086ade9d32fa}"),
                    Date = new DateTime(2020, 4, 12, 13, 0, 0),
                    Duration = TimeSpan.FromHours(3),
                    Gate = "B5",
                    Number = "A21362",
                    From = airports.ElementAt(0),
                    To = airports.ElementAt(1),
                    Plane = planes.ElementAt(1)
                },
                new Flight()
                {
                    Id = Guid.Parse("{32a87c17-fa89-4c7b-9bde-9f0379ef8b78}"),
                    Date = new DateTime(2020, 8, 22, 16, 12, 0),
                    Duration = TimeSpan.FromHours(2),
                    Gate = "O36",
                    Number = "B12351",
                    From = airports.ElementAt(1),
                    To = airports.ElementAt(2),
                    Plane = planes.ElementAt(2)
                },
                new Flight()
                {
                    Id = Guid.Parse("{230045b8-d573-4c7c-8e1e-7f68a642ceb1}"),
                    Date = new DateTime(2020, 2, 24, 7, 11, 0),
                    Duration = TimeSpan.FromHours(16),
                    Gate = "c22",
                    Number = "A12314",
                    From = airports.ElementAt(2),
                    To = airports.ElementAt(3),
                    Plane = planes.ElementAt(3)
                },
                new Flight()
                {
                    Id = Guid.Parse("{c7a9acf6-12db-4bff-b54e-47d423cddbbb}"),
                    Date = new DateTime(2020, 2, 13, 8, 24, 0),
                    Duration = TimeSpan.FromHours(3),
                    Gate = "D11",
                    Number = "B72164",
                    From = airports.ElementAt(3),
                    To = airports.ElementAt(4),
                    Plane = planes.ElementAt(4)
                },
                new Flight()
                {
                    Id = Guid.Parse("{97053acf-0390-4773-8501-e3c69e4ef9c9}"),
                    Date = new DateTime(2020, 2, 16, 9, 30, 0),
                    Duration = TimeSpan.FromHours(6),
                    Gate = "s66",
                    Number = "A27134",
                    From = airports.ElementAt(4),
                    To = airports.ElementAt(5),
                    Plane = planes.ElementAt(5)
                },
                new Flight()
                {
                    Id = Guid.Parse("{7d689931-9cd5-4e93-8573-07b048ca62c4}"),
                    Date  = new DateTime(2020, 2 , 19, 11, 45, 0),
                    Duration = TimeSpan.FromHours(4),
                    Gate = "e14",
                    Number = "A22112",
                    From = airports.ElementAt(5),
                    To = airports.ElementAt(6),
                    Plane = planes.ElementAt(0)
                }
            };
        }

        [HttpGet]
        public IEnumerable<Flight> Get()
        {
            return flights;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid id)
        {
            var flight = flights?.FirstOrDefault(f => f.Id == id);

            if (flight != null)
                return Ok(flight);
            else
                return NotFound();
        }
    }
}