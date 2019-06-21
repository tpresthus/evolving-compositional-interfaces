using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Customers
{
    public class CustomerApiController : Controller
    {
        private readonly CustomerRepository customerRepository;

        public CustomerApiController(CustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public object Index()
        {
            var customers = this.customerRepository.GetCustomers();
            var operationFactory = new OperationFactory(Url);
            var customerListResponse = new CustomerListResponse(customers, operationFactory);
            return customerListResponse;
        }

        [Route("{id}")]
        public object Customer(string id)
        {
            if (id == "favicon.ico")
            {
                // Explicitly ignore favicon.ico without throwing exceptions
                return NotFound();
            }

            var customer = this.customerRepository.GetCustomer(id);
            var operationFactory = new OperationFactory(Url);
            var customerResponse = new CustomerResponse(customer, operationFactory);
            return customerResponse;
        }

        [HttpPut]
        [Route("{id}")]
        public object Update([FromRoute] string id, [FromBody] IDictionary<string, string> values)
        {
            var customer = this.customerRepository.GetCustomer(id);
            customer = customer.Hydrate(values);
            customer = this.customerRepository.Update(customer);
            var operationFactory = new OperationFactory(Url);
            var customerResponse = new CustomerResponse(customer, operationFactory);
            return customerResponse;
        }
    }
}
