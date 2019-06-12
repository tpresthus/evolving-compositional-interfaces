using System.Collections.Generic;

namespace Customers
{
    public class CustomerListResponse
    {
        public CustomerListResponse(IEnumerable<Customer> customers)
        {
            Customers = customers;
        }

        public IEnumerable<Customer> Customers { get; }
    }
}
