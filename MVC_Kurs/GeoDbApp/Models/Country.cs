﻿namespace GeoDbApp.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Population { get; set; }
        public string Capitol { get; set; }

        public int ContinentId { get; set; }

        public Continent ContinetRef { get; set; }



        public List<LanguagesInCountry> LanguagesInCountry { get; set; }
    }
}
