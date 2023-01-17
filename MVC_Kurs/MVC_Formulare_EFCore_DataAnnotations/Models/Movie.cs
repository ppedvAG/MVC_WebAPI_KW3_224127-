
using MVC_Formulare_EFCore_DataAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Formulare_EFCore_DataAnnotations.Models
{
    //[Table("FilmeTabelle", Schema = "vc")] -> Tabellennamen kann man auch via Data Annotations definiert 
    //Eine Weitere Variante, wäre in DbContext via FluentAPI -> https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx
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
        [ClassicMovie(1960)]
        public DateTime ReleaseDate { get; set; }

        public GenreTyp Genre { get; set; }


        [Required]
        [UniversalSerialnumber()]
        public string SerialNumber { get; set; }
    }

    public enum GenreTyp { Action, ScienceFiction, Comedy, Drama, Horror, Romance, Classic, Animiation, Doku}
}
