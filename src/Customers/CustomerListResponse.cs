using System;
using System.Collections.Generic;
using System.Linq;

namespace Customers
{
    public class CustomerListResponse
    {
        public CustomerListResponse(IEnumerable<Customer> customers, OperationFactory operationFactory)
        {
            if (customers == null)
            {
                throw new System.ArgumentNullException(nameof(customers));
            }

            if (operationFactory == null)
            {
                throw new ArgumentNullException(nameof(operationFactory));
            }

            Customers = customers.Select(customer => new CustomerResponse(customer, operationFactory));
            Id = operationFactory.CreateOperation(customers).Target;
        }

        public Uri Id { get; }

        public IEnumerable<CustomerResponse> Customers { get; }
    }
}
