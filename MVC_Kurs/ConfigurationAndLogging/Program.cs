using ConfigurationAndLogging.Configurations;
using System.Drawing.Printing;

namespace ConfigurationAndLogging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); //Wichtig -> Appsettings.json ist hier schon verfügbar :-) 

            //SAMPLE -> ConfigurationController->Sample2
            builder.Services.Configure<PositionOptions>(
                builder.Configuration.GetSection(PositionOptions.Position));


            //Sample -> ConfigurationController->Sample3
            //Einlesen einer zusätzlichen Konfigurationsquelle 
            builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("GameSettings.json", optional: true, reloadOnChange: true);
            });

            builder.Services.Configure<GameSettings>(
                builder.Configuration.GetSection("GameSettings"));

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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}