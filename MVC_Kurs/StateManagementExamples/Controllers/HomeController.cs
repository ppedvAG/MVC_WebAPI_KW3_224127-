using Microsoft.AspNetCore.Mvc;
using StateManagementExamples.Models;
using System.Diagnostics;

namespace StateManagementExamples.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //Wenn in TempData[Email]hier leer ist, werden wir direkt auf die Initalisierungs-Seite zurück geleitet
            if (TempData["Email"] == null)
            {
                return RedirectToAction("TempDataSample", "Statemanagement");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}