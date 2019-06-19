using System;
using Microsoft.AspNetCore.Mvc;

namespace Customers
{
    [Route("profiles")]
    public class CustomerProfileController : Controller
    {
        private readonly CustomerRepository customerRepository;

        public CustomerProfileController (CustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }
        
        [Route("{id}")]
        public IActionResult Customer(string id)
        {
            var customer = this.customerRepository.GetCustomer(id);

            return View("ProfileCard", customer);
        }
    }
}