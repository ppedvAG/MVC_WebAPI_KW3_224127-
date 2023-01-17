using Microsoft.AspNetCore.Mvc;
using MVC_Einstieg.Models;
using MVC_Einstieg.ViewModels;

namespace MVC_Einstieg.Controllers
{
    public class SimpleDataController : Controller
    {
        //Einfache Übergabe einer Person an die View
        public IActionResult Sample1()
        {
            //Optional könnten wir auch via EF Core auf eine Datenbank zugreifen und von dort ein Person-Objekt befüllt bekommen und weiterreichen 

            Person person = new Person("Otto Walkes", 65);

            return View(person);
        }

        public IActionResult TableWithModel()
        {
            IList<Person> list = new List<Person>();
            list.Add(new Person("Max Mustermann", 44));
            list.Add(new Person("Moni Musterfrau", 42));
            list.Add(new Person("Helge Schneider", 54));
            list.Add(new Person("Otto Walkes", 51));


            return View(list);
        }

        public IActionResult GeneratedTableWithModel()
        {
            IList<Person> list = new List<Person>();
            list.Add(new Person("Max Mustermann", 44));
            list.Add(new Person("Moni Musterfrau", 42));
            list.Add(new Person("Helge Schneider", 54));
            list.Add(new Person("Otto Walkes", 51));


            return View(list);
        }


        public IActionResult GenerateTableWithViewModel()
        {
            MovieViewModel vm = new MovieViewModel();

            vm.Movie = new Movie
            {
                Id = 1,
                Title = "Jurrasic Park",
                Description = "TRex wird Veggie",
                Price = 19.99m
            };

            vm.Cast = new List<Artists>();


            vm.Cast.Add(new Artists
            {
                Id = 1,
                FirstName = "Otto",
                LastName = " Walkes"
            });

            vm.Cast.Add(new Artists
            {
                Id = 2,
                FirstName = "Harry",
                LastName = " Weinfuhrt"
            });

            vm.Cast.Add(new Artists
            {
                Id = 3,
                FirstName = "Ralf",
                LastName = " Möller"
            });

            vm.ExterneIMDBRating = 8;


            return View(vm);
        }
    }
}
