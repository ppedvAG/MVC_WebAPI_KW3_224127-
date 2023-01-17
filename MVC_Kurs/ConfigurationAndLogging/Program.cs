using ConfigurationAndLogging.Configurations;
using Serilog;
using System.Drawing.Printing;

namespace ConfigurationAndLogging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); //Wichtig -> Appsettings.json ist hier schon verf�gbar :-) 

            #region Konfiguration Samples
            //SAMPLE -> ConfigurationController->Sample2
            builder.Services.Configure<PositionOptions>(
                builder.Configuration.GetSection(PositionOptions.Position));


            //Sample -> ConfigurationController->Sample3
            //Einlesen einer zus�tzlichen Konfigurationsquelle 
            builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("GameSettings.json", optional: true, reloadOnChange: true);
            });

            builder.Services.Configure<GameSettings>(
                builder.Configuration.GetSection("GameSettings"));



            #endregion


            #region Logging-Sample

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>(optional: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();


            builder.Host.UseSerilog();

            #endregion
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


            try
            {
                app.Run();
            }
            catch( Exception ex)
            {
                Log.Error(ex.ToString());
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }
    }
}