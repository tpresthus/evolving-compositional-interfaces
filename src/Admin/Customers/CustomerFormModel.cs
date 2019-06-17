namespace Admin.Customers
{
    public class CustomerFormModel
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Website { get; set; }
        public CustomerAddressFormModel Address { get; set; }

        public Customer Map(string id)
        {
            return new Customer(id, Name, BirthDate)
            {
                Email = Email,
                Phone = Phone,
                UserName = UserName,
                Website = Website,
                Address = Address.Map()
            };
        }
    }
}
