using Microsoft.AspNetCore.Mvc;

namespace Admin.Users
{
    [Route("users")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
