using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoApp.Shared.Entities
{

    //Wichtig bei EF Core: 
    public class Country
    {
        [Key] //Key wird nur optional benötigt -> EF Core schaut ob es eine Property mit Id oder CountryId gibt. (Konvention) 
        public int Id { get; set; }

        public string Name { get; set; }

        public string Capitol { get; set; }

        public int? ContinentId { get; set; }

        //public virtual Continent -> virtual verwende ich, weil ich mit Lazy Loading arbeite -> Relationen verwende 

        public virtual Continent? ContinentRef { get; set; } 
    }
}
