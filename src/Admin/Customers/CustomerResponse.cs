using System;

namespace Admin.Customers
{
    public class CustomerResponse
    {
        public CustomerResponse(Customer customer, string json)
        {
            if (String.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentNullException(nameof(json));
            }

            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Json = json;

        }
        public Customer Customer { get; }
        public string Json { get; }
    }
}
