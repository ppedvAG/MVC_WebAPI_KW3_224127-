using Microsoft.EntityFrameworkCore;
using WebAPI_with_EFCore_Relationen_EagerLoading.Models;

namespace WebAPI_with_EFCore_Relationen_EagerLoading.Data
{
    public class GeoDbContext : DbContext
    {
        public GeoDbContext(DbContextOptions<GeoDbContext> options)
            : base(options)
        {
        }


        public DbSet<Continent> Continents { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LanguagesInCountry> LanguagesInCountries { get; set; }
    }
}
