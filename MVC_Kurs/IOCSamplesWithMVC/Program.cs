using IOCSamplesWithMVC.Services;
using IOCSamplesWithMVC.Services.Extentions;

namespace IOCSamplesWithMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region ServiceCollection with Singleton / Scope / Transient 
            //Singleton: Einmal wird die Instanz erstellt und die Instanz ist immer abrufbar. 
            builder.Services.AddSingleton<ITimeService, TimeService>();


            //MIT ERWEITERUNGSMETHODE 
            builder.Services.AddTimeService();

            //Scoped: Pro Request wird 1x das Objekt neu gebaut 
            //builder.Services.AddScoped<ITimeService, TimeService2>(); //Wenn ich hier auf ITimeSerivce zugreife, bekomme ich TimeService2 geliefert (TimeSerivce wird durch TimeServie2 überschrieben)

            //Transient: 1x Pro Zugriff auf IOC-Container wird das Object neu gebaut
            //builder.Services.AddTransient<ITimeService, TimeService>();
            #endregion

            #region One Interface, multiple Implementations 
            //PROBLEMSTELLUNG -> ONE INTERFACE AND MULTIPLE IMPLEMENTATIOMS 
            //Lösungsansatz 1 -> https://medium.com/geekculture/net6-dependency-injection-one-interface-multiple-implementations-983d490e5014
            //Lösungsansatz 2 -> https://learn.microsoft.com/de-de/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-7.0

            //Lösung mit Tränen in Augen: -> https://thecodeblogger.com/2022/09/16/net-dependency-injection-one-interface-and-multiple-implementations/
            //Mit dem Nachteil: Langsamer, weil nicht typisiert ist und Erweiterungen benötigen bei der Implementierung mehr Zeit  
            #endregion



            // Add services to the container
            // //builder.Services -> IServiceCollection 
            builder.Services.AddControllersWithViews();

            var app = builder.Build(); //Initialsierung des IOC Containers ist hiermit beendet

            #region Frühester Zugriff auf IOC Container -> Via app.Servies.GetServices/GetRequiredService + IServiceScope  
            //Use Case u.a. Für Testdaten, wenn wir EFCore verwenden 
            //Allerdings kann man auf Objekte die den LifeCycle mit Scoped verwenden hier nicht auflösen. Weil es sich hierbei um kein Request handelt
            ITimeService? service1 = app.Services.GetService<ITimeService>();


            ITimeService service2 = app.Services.GetRequiredService<ITimeService>();
            

            //ab .NET 2.1 -> Kann mit Scope-Lifetime Objects arbeiten 
            using (IServiceScope scope = app.Services.CreateScope())
            {
                ITimeService service3 = scope.ServiceProvider.GetService<ITimeService>();
                ITimeService service4 = scope.ServiceProvider.GetRequiredService<ITimeService>();
            }

            #endregion


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