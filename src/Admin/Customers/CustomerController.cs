using System;
using System.Threading.Tasks;
using Admin.Authorization;
using Admin.Navigation;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Customers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly CustomerService customerService;
        private readonly NavigationService navigationService;
        private readonly AuthorizationService authorizationService;

        public CustomerController(CustomerService customerService, NavigationService navigationService, AuthorizationService authorizationService)
        {
            this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
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
    }
}
