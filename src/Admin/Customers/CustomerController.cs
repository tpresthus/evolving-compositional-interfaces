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
            var customerViewModel = await GetCustomerViewModel(id);
            return View(customerViewModel);
        }

        [Route("{id}/edit")]
        public async Task<IActionResult> Edit(string id)
        {
            var customerViewModel = await GetCustomerViewModel(id);
            return View(customerViewModel);
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromForm] CustomerFormModel customerFormModel)
        {
            if (!ModelState.IsValid)
            {
                var customerViewModel = await GetCustomerViewModel(id);
                return View("Edit", customerViewModel);
            }
            
            var customer = customerFormModel.Map(id);
            await this.customerService.UpdateCustomer(customer);
            return RedirectToAction("Customer", new { Id = id });
        }

        private async Task<CustomerViewModel> GetCustomerViewModel(string id)
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var customerResponse = await this.customerService.GetCustomer(id);
            var urlFactory = new UrlFactory(Url);
            var transactions = await this.transactionService.GetTransactionsForCustomer(id);
            return new CustomerViewModel(customerResponse, menu, user, urlFactory, transactions);
        }
    }
}
