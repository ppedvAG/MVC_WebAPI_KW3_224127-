using GeoApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeoApp.UI.Controllers
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
            
                //using (HttpClient client = new HttpClient()) 
                //{
                
                
                //} //Dispose wird auferufen -> Dispose das Objekt ab


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}