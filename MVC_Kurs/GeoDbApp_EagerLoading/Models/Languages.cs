namespace GeoDbApp.Models
{
    public class Language
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public List<LanguagesInCountry> LanguagesInCountry { get; set; }
    }
}
