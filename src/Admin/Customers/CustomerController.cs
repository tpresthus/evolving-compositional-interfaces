using System;
using System.Linq;
using System.Threading.Tasks;
using Admin.Authorization;
using Admin.Navigation;
using Admin.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Customers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly CustomerService customerService;
        private readonly TransactionService transactionService;
        private readonly NavigationService navigationService;
        private readonly AuthorizationService authorizationService;

        public CustomerController(CustomerService customerService, TransactionService transactionService, NavigationService navigationService, AuthorizationService authorizationService)
        {
           this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            this.transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
             this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public async Task<IActionResult> Index()
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var customers = await this.customerService.GetCustomers();
            var urlFactory = new UrlFactory(Url);

            var customersViewModel = new CustomersViewModel(customers, menu, user, urlFactory);

            return View(customersViewModel);
        }

        [Route("{id}")]
        public async Task<IActionResult> Customer(string id)
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var customers = await this.customerService.GetCustomers();
            // TODO: Add customer/id route to the Customer API.
            var customer = customers.First(c => c.Id == id);
            var urlFactory = new UrlFactory(Url);

            var customersViewModel = new CustomerViewModel(customer, menu, user, urlFactory);

            return View(customersViewModel);
        }
    }
}
