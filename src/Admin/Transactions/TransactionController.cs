using Microsoft.AspNetCore.Mvc;

namespace Admin.Transactions
{
    [Route("transactions")]
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
