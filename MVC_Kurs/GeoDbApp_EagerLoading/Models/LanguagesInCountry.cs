using System.ComponentModel.DataAnnotations;

namespace GeoDbApp.Models
{
    public class LanguagesInCountry
    {
        public int Id { get; set; } 
        public int CountryId { get; set; }  
        public int LanguageId { get; set; }

        [Range(1, 100)]
        public int Percent { get; set; } //Soll darstellen, wie Weit die Sprache in einem Land verbreitet ist. 


        public Country Country { get; set; }

        public Language Language { get; set; }    
    }
}
