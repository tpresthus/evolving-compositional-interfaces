using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Errors
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View(new ErrorViewModel
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
        
    }
}