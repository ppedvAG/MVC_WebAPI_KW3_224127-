using System.ComponentModel.DataAnnotations;

namespace RoutingSamples.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [Range(0.1, 100)]
        public decimal Price { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public GenreTyp Genre { get; set; }


        [Required(ErrorMessage = "Bist du eingeschlafen")]

        public string SerialNumber { get; set; }
    }

    public enum GenreTyp { Action, ScienceFiction, Comedy, Drama, Horror, Romance, Classic, Animiation, Doku }
}
