using System;

namespace Admin.Customers
{
    public class Customer
    {
        public Customer(string name, string email, DateTime birthDate)
        {
            BirthDate = new DateOfBirth(birthDate);
            Email = new Email(email);
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public Email Email { get; }

        public DateOfBirth BirthDate { get; }
    }
}
