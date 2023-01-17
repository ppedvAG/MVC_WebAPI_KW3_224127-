using Microsoft.Extensions.Hosting;

namespace GeoDbApp.Models
{
    public class Continent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Country> Countries { get; set; }
    }
}
