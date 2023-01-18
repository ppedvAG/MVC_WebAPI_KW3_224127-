using Microsoft.AspNetCore.Mvc;

namespace RazorAdvanced.Controllers
{
    public class PartialViewSamplesController : Controller
    {
        public IActionResult PartialViewSample1()
        {
            return View();
        }
    }
}
