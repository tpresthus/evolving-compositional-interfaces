namespace Admin.Customers
{
    public class CustomerAddressFormModel
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }

        public Address Map()
        {
            return new Address(Street, City, ZipCode, State);
        }
    }
}
