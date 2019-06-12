using System;
using System.Collections.Generic;

namespace Admin.Customers
{
    public class Customer
    {
        public Customer(string id, string name, string email, string birthDate)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            BirthDate = DateOfBirth.Parse(birthDate);
            Email = new Email(email);
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Id { get; }

        public string Name { get; }

        public Email Email { get; }

        public DateOfBirth BirthDate { get; }
    }
}
