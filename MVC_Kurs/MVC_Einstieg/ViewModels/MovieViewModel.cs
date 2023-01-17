using MVC_Einstieg.Models;

namespace MVC_Einstieg.ViewModels
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; } 

        public List<Artists> Cast { get; set; } 

        public int ExterneIMDBRating { get; set; }
    }
}
