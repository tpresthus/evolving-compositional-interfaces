using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Admin.Authorization;
using Admin.Errors;

namespace Admin.Home
{
    public class HomeController : Controller
    {
        private readonly AuthorizationService authorizationService;

        public HomeController(AuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }
            
        public async Task<IActionResult> Index()
        {
            var user = await this.authorizationService.GetAuthorizedUser();
            var homeViewModel = new HomeViewModel(user);
            return View(homeViewModel);
        }
    }
}