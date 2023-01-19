namespace WebAPI_with_EFCore_Relationen_EagerLoading.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Population { get; set; }
        public string Capitol { get; set; }

        public int ContinentId { get; set; }

        public virtual Continent ContinetRef { get; set; }



        public virtual List<LanguagesInCountry> LanguagesInCountry { get; set; }
    }
}
