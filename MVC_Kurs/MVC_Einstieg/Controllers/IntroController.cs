using Microsoft.AspNetCore.Mvc;

namespace MVC_Einstieg.Controllers
{
    public class IntroController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hallo Welt. Ich verwende keine _Layout-Page. Ich kenne kein HMTL");
        }

        public IActionResult Sample2()
        {
            return View();
        }

        public IActionResult Sample3()
        {
            return View();
        }
    }
}
