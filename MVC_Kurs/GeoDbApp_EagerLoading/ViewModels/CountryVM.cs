using GeoDbApp.Models;

namespace GeoDbApp.ViewModels
{
    public class CountryVM
    {
        public Country CurrentCountry { get; set; }

        public IList<Continent> ContinentList { get; set;} //Für das setzten des Country.ContinentId 
    }
}
