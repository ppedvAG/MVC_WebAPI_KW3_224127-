using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_Einstieg.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;
      
        public decimal Price { get; set; }

        public GenreTyp Genre { get; set; }
    }

    public enum GenreTyp { Action, Drama, Thriller, Horror, Roamnce, Comedy, Animations, Documentation }

    
}
