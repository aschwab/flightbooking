using System;

namespace FBS.Domain.Authorization
{
    public class User
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }
    }
}