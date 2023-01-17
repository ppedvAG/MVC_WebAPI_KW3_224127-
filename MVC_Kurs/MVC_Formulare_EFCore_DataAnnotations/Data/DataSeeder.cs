using MVC_Formulare_EFCore_DataAnnotations.Models;
namespace MVC_Formulare_EFCore_DataAnnotations.Data
{
    public static class DataSeeder
    {
        public static void SeedMovieStoreDb(MovieDbContext context)
        {
            //Befinden sich Datensätze in der Tabelle "Movies" 
            if(!context.Movies.Any())
            {
                context.Movies.Add(new Movie { Title = "Alice im Wunderland", Description = "Wo ist der Uhrmacher", Price = 10, Genre = GenreTyp.Animiation, ReleaseDate =  DateTime.Now, SerialNumber="EG-1234" });
                context.Movies.Add(new Movie { Title = "Amsterdam", Description = "Ist wirklich passiert", Price = 12, Genre = GenreTyp.Action, ReleaseDate = DateTime.Now, SerialNumber = "EU-1234" });
                context.Movies.Add(entity: new Movie { Title = "Operation Fortune", Description = "Auch ein neuer Film", Price = 11, Genre = GenreTyp.Action, ReleaseDate = DateTime.Now, SerialNumber = "AS-1234" });
            }

            context.SaveChanges();
        }
    }
}
