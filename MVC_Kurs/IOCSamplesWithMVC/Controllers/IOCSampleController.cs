using IOCSamplesWithMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace IOCSamplesWithMVC.Controllers
{
    //Wie greife ich auf den IOC-Container via Controller-Klasse zu?

    public class IOCSampleController : Controller
    {
        private ITimeService klassenweiterTimeService; //ÜBerall von anderen Methoden dieser Klasse verwendbar

        //Variante 1 -> Konstruktor Injections
        public IOCSampleController(ITimeService timeService) 
        {
            klassenweiterTimeService = timeService;
        }



        //Variante 2: Methoden  - Injection -> Wenn ein Dienst oder eine Funktionalität nur innerhalb einer Methode verwendet wird
        public IActionResult Index([FromServices] ITimeService methodenWeiterTimeService)
        {
            string currentTime = methodenWeiterTimeService.GetObjectInstanceTime();

            return View();
        }


        public IActionResult Index2()
        {
            //Via HttpContext - GetService
            ITimeService service1 = this.HttpContext.RequestServices.GetService<ITimeService>();


            //oder 


            //Via HttpContext - GetRequiredService
            ITimeService service2 = this.HttpContext.RequestServices.GetRequiredService<ITimeService>();


            return View();
        }
    }
}
