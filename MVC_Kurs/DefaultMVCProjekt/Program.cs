namespace DefaultMVCProjekt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Wo finde ich MVC Spezifische Einträge? 
            //1.) builder.Services.AddControllersWithViews();               -> Was für ein UI-Framework wird verwendet
            //2.) app.MapControllerRoute(                                   -> Wie kommt mein Request bei MVC zum richtigen Controller/Action-Methode
            //      name: "default",
            //      pattern: "{controller=Home}/{action=Index}/{id?}");



            #region Initialisierung des IOC-Containers

            // - appsetting.json ist hier schon eingelesen und man könnte hier schon darauf zugreifen
            // - WebApplication.CreateBuilder ist eine Factory Methode und gibt uns WebApplicationBuilder als Instanz zurück

            // - WebApplicationBuilder Klasse ist für die Initialsiierung unserer WebApp verantwortlich (wird in RazorPages, MVC, WebAPI, BlazorServer)
            // - Der WebApplicationBuilder ist Abwärtskompatibel zu den anderen ASP.NET Core Versionen. 


            WebApplicationBuilder builder = WebApplication.CreateBuilder(args); 

            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Abwärtskompatibel zu .NET 3.1 / 5.0 
            //builder.Host = CreateHostBuilder(args); // IHostBuilder 

            //Abwärtskompatibel zu .NET 2.x 
            //builder.WebHost = BuildWebHost()

            /*
             * public static IWebHost BuildWebHost(string[] args)
                {
                    WebHost.CreateDefaultBuilder(args)
                        .UseStartup<Startup>()
                        .Build();
                }
             * 
             */

            WebApplication app = builder.Build();
            #endregion


            #region Konfigurationen der Dienste und MVC Framework

            //WebApplication wird konfiguriert 

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
            #endregion


            //Die App wird gestarten und hoffen Sie läuft 24/7 
            app.Run();
        }
    }
}