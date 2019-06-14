namespace Customers
{
    public class CustomerFormModel
    {
        public string Name { get; set; }
        public AddressFormModel Address { get; set; }
        public string Ssn { get; set; }
        public string Phone { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Website { get; set; }

        public Customer Map(string id)
        {
            var customer = new Customer
            {
                Id = id,
                Name = Name,
                Ssn = Ssn,
                Phone = Phone,
                BirthDate = BirthDate,
                Email = Email,
                UserName = UserName,
                Website = Website
            };

            if (Address != null)
            {
                customer.Address = new Address
                {
                    Street = Address.Street,
                    City = Address.City,
                    ZipCode = Address.ZipCode,
                    State = Address.State
                };
            }

            return customer;
        }
    }
}