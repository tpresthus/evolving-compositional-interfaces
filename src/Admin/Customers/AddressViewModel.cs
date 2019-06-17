using System;

namespace Admin.Customers
{
    public class AddressViewModel
    {
        public AddressViewModel(Address address)
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
