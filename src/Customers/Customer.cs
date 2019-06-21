using System;
using System.Collections.Generic;

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

        public Customer Hydrate(IDictionary<string, string> values)
        {
            Name = values["https://schema.org/name"];
            Phone = values["https://schema.org/telephone"];
            BirthDate = values["https://schema.org/birthDate"];
            Email = values["https://schema.org/email"];
            UserName = values["https://schema.org/alternateName"];
            Website = values["https://schema.org/WebSite"];

            Address?.Hydrate(values);

            return this;
        }
    }
}
