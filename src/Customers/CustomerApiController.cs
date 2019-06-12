using System;
using Microsoft.AspNetCore.Mvc;

namespace Customers
{
    public class CustomerApiController : Controller
    {
        private readonly CustomerRepository customerRepository;

        public CustomerApiController (CustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public IActionResult Index()
        {
            var customers = this.customerRepository.GetCustomers();
            var customerListResponse = new CustomerListResponse(customers);
            return Json(customerListResponse);
        }

        [Route("{id}")]
        public IActionResult Customer(string id)
        {
            var customer = this.customerRepository.GetCustomer(id);

            if (customer == null)
            {
                return Json(new { Text = "Not found!"});
            }

            return Json(customer);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] string id, [FromBody] CustomerFormModel customerFormModel)
        {
            var customer = customerFormModel.Map(id);

            try
            {
                customer = this.customerRepository.Update(customer);
            }
            catch (CustomerNotFoundException)
            {
                return NotFound();
            }

            return Json(customer);
        }
    }
}