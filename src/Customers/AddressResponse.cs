using System;

namespace Customers
{
    public class AddressResponse
    {
        public AddressResponse(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            Street = address.Street;
            City = address.City;
            ZipCode = address.ZipCode;
            State = address.State;
        }

        public string Street { get; }
        public string City { get; }
        public string ZipCode { get; }
        public string State { get; }
    }
}
