using System;
using System.Collections.Generic;

namespace Admin.Customers
{
    public class Customer
    {
        public Customer(string id, string name, string birthDate)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            BirthDate = DateOfBirth.Parse(birthDate);
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Id { get; }

        public string Name { get; }

        public Email Email { get; set; }

        public DateOfBirth BirthDate { get; }

        public string Ssn { get; set; }

        public string Phone { get; set; }

        public Address Address { get; set; }

        public string UserName { get; set; }

        public string Website { get; set; }
    }
}
