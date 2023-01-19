using Microsoft.Extensions.Hosting;

namespace WebAPI_with_EFCore_Relationen_LazyLoading.Models
{
    public class Continent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Country> Countries { get; set; }
    }
}
