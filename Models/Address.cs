﻿namespace Proiect.Models
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
