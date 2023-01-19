using System.ComponentModel.DataAnnotations;

namespace WebAPI_with_EFCore_Relationen_EagerLoading.Models
{
    public class LanguagesInCountry
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int LanguageId { get; set; }

        [Range(1, 100)]
        public int Percent { get; set; } //Soll darstellen, wie Weit die Sprache in einem Land verbreitet ist. 


        public virtual Country Country { get; set; }

        public virtual Language Language { get; set; }
    }
}
