using System;

namespace Admin.Customers
{
    public class Address
    {
        public Address(string street, string city, string zipCode, string state)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(city));
            ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
            State = state ?? throw new ArgumentNullException(nameof(state));

        }
        public string Street { get; }
        public string City { get; }
        public string ZipCode { get; }
        public string State { get; }
    }
}
