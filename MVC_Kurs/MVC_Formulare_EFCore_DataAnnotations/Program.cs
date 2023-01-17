using Microsoft.EntityFrameworkCore;
using MVC_Formulare_EFCore_DataAnnotations.Data;

namespace MVC_Formulare_EFCore_DataAnnotations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Wir legen unsere MovieDbContext-Klasse in den IOC Container
            builder.Services.AddDbContext<MovieDbContext>(options =>
            {
                //Datenbank im Speicher
                //options.UseInMemoryDatabase("MovieDb");

                //MovieDbConnectionString

                options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDbConnectionString"));
            });

            var app = builder.Build();

            //Testdaten hinzugenommen
            using (IServiceScope scope = app.Services.CreateScope())
            {
                MovieDbContext dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
                

                DataSeeder.SeedMovieStoreDb(dbContext);
                //Geht auch: 
                //MovieDbContext? dbContext2 = scope.ServiceProvider.GetService<MovieDbContext>();
            }

            //Problem ist, dass DbContext als Scoped verwendet wird. Scope allerdings sagt aus, dass das Objekt (DbContext) einmal pro Request instanziiert wird.
            //Allerdings befinden wir uns hier im Initializierung Part und finden kein Request vor. 
            //Als Workaround bietet sich IServiceScope mit app.Services.CreateScope an 

            //MovieDbContext? dbContext3 = app.Services.GetService<MovieDbContext>();
            //MovieDbContext dbContext4 = app.Services.GetRequiredService<MovieDbContext>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}