using Microsoft.AspNetCore.Mvc;

namespace Admin.Customers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
