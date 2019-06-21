using System.Collections.Generic;

namespace Customers
{
    public class Address
    {
        public string Street { get; internal set; }
        public string City { get; internal set; }
        public string ZipCode { get; internal set; }
        public string State { get; internal set; }

        public Address Hydrate(IDictionary<string, string> values)
        {
            Street = values["https://schema.org/streetAddress"];
            City = values["https://schema.org/addressLocality"];
            ZipCode = values["https://schema.org/postalCode"];
            State = values["https://schema.org/addressRegion"];
            return this;
        }
    }
}