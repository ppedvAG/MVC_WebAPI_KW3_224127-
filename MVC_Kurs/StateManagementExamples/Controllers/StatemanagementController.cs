
//#nullable disable
using Microsoft.AspNetCore.Mvc;
using StateManagementExamples.Models;

namespace StateManagementExamples.Controllers
{
    public class StatemanagementController : Controller
    {
        public IActionResult ViewDataSample()
        {
            ViewData.Add("Lottozahlen", "1234567");
            ViewData.Add("MitarbeiterDesMonats",new Person() { Vorname = "Max", Nachname = "Mustermann" });
            return View();
        }

        public IActionResult ViewBagSample() 
        {
            ViewBag.PersonDerWoche = new Person() { Vorname = "Donald", Nachname = "Duck" };
            return View();
        }

        public IActionResult TempDataSample()
        {
            TempData.Add("Email", "kevinw@ppedv.de");
            TempData.Add("Sportart", "Joggen");
            return View();
        }

        public IActionResult TempDataSample1()
        {
            return View();
        }

        public IActionResult TempDataSample2()
        {
            return View();
        }

        public IActionResult SessionStartSample()
        {
            //Einstiegsbeispiele
            HttpContext.Session.SetString("MitarbeiterDesMonats", "Donald Duck");
            HttpContext.Session.SetInt32("Lottozahlen", 1234567);

            //komplexes Objekt
            Person person = new Person() { Vorname = "Dagobert", Nachname = "Duck" };
            string json = System.Text.Json.JsonSerializer.Serialize(person);
            HttpContext.Session.SetString("MitarbeiterDesJahres", json);

            return View();
        }
        //Im HomeController->Index wird die Session intialisiert
        public IActionResult SessionOutputSample()
        {
            string mitarbeiterDesMonats = HttpContext.Session.GetString("MitarbeiterDesMonats");
            
            //Weiteres auslesen auf der View 
            return View();
        }
    }
}
