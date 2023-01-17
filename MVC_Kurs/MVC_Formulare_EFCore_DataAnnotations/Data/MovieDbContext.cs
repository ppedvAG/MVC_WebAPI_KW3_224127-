using Microsoft.EntityFrameworkCore;
using MVC_Formulare_EFCore_DataAnnotations.Models;

namespace MVC_Formulare_EFCore_DataAnnotations.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            :base(options) //Was steht in Options? -> ConnectionString zur DB
        {

        }

        //Per Konvention können wir via Propertyname den Tabellennamen definieren
        public DbSet<Movie> Movies { get; set; }
    }
}
