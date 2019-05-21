using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Navigation
{
    public class NavigationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(MenuViewModel menu)
        {
            return View("Default", menu);
        }
    }
}
