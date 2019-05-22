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
        private readonly NavigationService navigationService;
        private readonly AuthorizationService authorizationService;

        public CustomerController(NavigationService navigationService, AuthorizationService authorizationService)
        {
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public async Task<IActionResult> Index()
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var urlFactory = new UrlFactory(Url);
            var customersViewModel = new CustomersViewModel(menu, user, urlFactory);

            return View(customersViewModel);
        }
    }
}
