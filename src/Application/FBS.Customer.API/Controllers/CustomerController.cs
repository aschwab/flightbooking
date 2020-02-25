using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FBS.Customer.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IEnumerable<FBS.Domain.Customer.Customer> customers;

        public CustomerController()
        {
            customers = new FBS.Domain.Customer.Customer[]
            {
                new FBS.Domain.Customer.Customer()
                {
                    Id = Guid.Parse("{611e53fa-a034-4374-873b-873f7b4de854}"),
                    FirstName = "Alexander",
                    LastName = "Schwab"
                },
                new FBS.Domain.Customer.Customer()
                {
                    Id = Guid.Parse("{89646861-7560-4774-80c2-e5dea17fcd8a}"),
                    FirstName = "Angelika",
                    LastName = "Grünwald"
                }
            };
        }

        [HttpGet]
        public IEnumerable<FBS.Domain.Customer.Customer> Get()
        {
            return customers;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public FBS.Domain.Customer.Customer Get(Guid id)
        {
            var customer = customers?.FirstOrDefault(f => f.Id == id);
            return customer;
        }
    }
}