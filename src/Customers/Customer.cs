using System;

namespace Customers
{
    public class Customer
    {
        public Customer(string id, string name, string ssn)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(ssn))
            {
                throw new ArgumentNullException(nameof(ssn));
            }

            Id = id;
            Name = name;
            Ssn = ssn;
        }

        public string Id { get; }
        public string Name { get; internal set; }
        public Address Address { get; internal set; }
        public string Ssn { get; }
        public string Phone { get; internal set; }
        public string BirthDate { get; internal set; }
        public string Email { get; internal set; }
        public string UserName { get; internal set; }
        public string Website { get; internal set; }
    }
}
