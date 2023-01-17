namespace GeoDbApp.Models
{
    public class Language
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public virtual List<LanguagesInCountry> LanguagesInCountry { get; set; }
    }
}
