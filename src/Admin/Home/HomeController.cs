using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Admin.Navigation;
using Admin.Authorization;

namespace Admin.Home
{
    public class HomeController : Controller
    {
        private readonly NavigationService navigationService;
        private readonly AuthorizationService authorizationService;

        public HomeController(NavigationService navigationService, AuthorizationService authorizationService)
        {
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public async Task<IActionResult> Index()
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var urlFactory = new UrlFactory(Url);
            var homeViewModel = new HomeViewModel(menu, user, urlFactory);

            return View(homeViewModel);
        }
    }
}
