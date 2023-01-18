using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoutingSamples.Data;
namespace RoutingSamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MoveDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MoveDbContext") ?? throw new InvalidOperationException("Connection string 'MoveDbContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

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
                  name: "movie",
                  pattern: "movie/{*movie}", defaults: new { controller = "Movie", action = "Index" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

           

            app.Run();
        }
    }
}