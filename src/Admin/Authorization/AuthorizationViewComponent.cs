using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Authorization
{
    public class AuthorizationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(UserViewModel user)
        {
            return View("Default", user);
        }
    }
}
