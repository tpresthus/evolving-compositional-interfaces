namespace Customers
{
    public class Customer
    {
        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public Address Address { get; internal set; }
        public string Ssn { get; internal set; }
        public string Phone { get; internal set; }
        public string BirthDate { get; internal set; }
        public string Email { get; internal set; }
        public string UserName { get; internal set; }
        public string Website { get; internal set; }
    }
}