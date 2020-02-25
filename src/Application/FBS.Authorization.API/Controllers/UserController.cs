using FBS.Domain.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBS.Authorization.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IEnumerable<User> user;

        public UserController()
        {
            this.user = new User[]
            {
                new User()
                {
                    Id = Guid.Parse("{fff9e10b-b949-4e78-88fd-6bd47b2790bc}"),
                    Name = "aschwab",
                    PasswordHash = "098f6bcd4621d373cade4e832627b4f6", //MD5: test
                    CustomerId = Guid.Parse("{611e53fa-a034-4374-873b-873f7b4de854}")
                },
                new User()
                {
                    Id = Guid.Parse("{4418f681-a162-40c9-94dc-a52987e564df}"),
                    Name = "agruen",
                    PasswordHash = "098f6bcd4621d373cade4e832627b4f6", //MD5: test
                    CustomerId = Guid.Parse("{89646861-7560-4774-80c2-e5dea17fcd8a}")
                }
            };
        }

        /// <summary>
        /// Attention! This is not a valid way to authorize a user in a production environment!
        /// This is for development / testing only!
        /// </summary>
        [HttpGet("/authorize")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Guid? Authorize(String userName, String hashedPassword)
        {
            var user = this.user?.FirstOrDefault(f => f.Name == userName);
            return (user != null && user.PasswordHash == hashedPassword) ? (Guid?)user.CustomerId : null;
        }
    }
}