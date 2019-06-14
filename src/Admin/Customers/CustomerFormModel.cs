namespace Admin.Customers
{
    public class CustomerFormModel
    {
        public string Name { get; set; }

        public string BirthDate { get; set; }

        public string Email { get; set; }

        public Customer Map(string id)
        {
            return new Customer(id, Name, Email, BirthDate);
        }
    }
}