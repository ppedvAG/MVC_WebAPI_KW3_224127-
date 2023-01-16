using IOCSamplesWithMVC.Services;

namespace IOCSamplesWithMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<ITimeService, TimeService>();


            // Add services to the container
            // //builder.Services -> IServiceCollection 
            builder.Services.AddControllersWithViews();

            var app = builder.Build(); //Initialsierung des IOC Containers ist hiermit beendet

            ITimeService service1 = app.Services.GetService<ITimeService>();
            ITimeService service2 = app.Services.GetRequiredService<ITimeService>();


            //ab .NET 2.1 
            using (IServiceScope scope = app.Services.CreateScope())
            {
                ITimeService service3 = scope.ServiceProvider.GetService<ITimeService>();
                ITimeService service4 = scope.ServiceProvider.GetRequiredService<ITimeService>();
            }


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