using GeoDbApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace GeoDbApp.Data
{
    public class GeoDbContext : DbContext
    {
        public GeoDbContext(DbContextOptions<GeoDbContext> options)
            :base(options)
        {

        }

        public DbSet<Continent> Continents { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LanguagesInCountry> LanguagesInCountries { get; set; }
    }
}
