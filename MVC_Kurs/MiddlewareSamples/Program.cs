namespace MiddlewareSamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            //Eigene Mittlewares vor dem Endpunkt MapControllerRoute reingesetzt


            #region IntroSample1
            //app.Use(async (context, next) =>
            //{
            //    // Do work that can write to the Response.
            //    await next.Invoke();
            //    // Do logging or other work that doesn't write to the Response.
            //});


            ////Run Terminiert die Middleware-Pipeline
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello from 2nd delegate.");
            //});


            #endregion

            #region Sample 2 Middlewares können je nach URL aufgerufen werden
            //app.Map("/map1", HandleMapTest1);

            //app.Map("/map2", HandleMapTest2);

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello from non-Map delegate.");
            //});

            //app.Run();


            #endregion


            #region Sample 3 Middle können auch auf Teile der URL reagieren
            app.MapWhen(context => context.Request.Query.ContainsKey("branch"), HandleBranch);
            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
            #endregion

            #region 
            //app.MapControllerRoute(
            //   name: "default",
            //   pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.Run();
            #endregion
        }

        static void HandleMapTest1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 1");
            });
        }

        static void HandleMapTest2(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 2");
            });
        }

        static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var branchVer = context.Request.Query["branch"];
                await context.Response.WriteAsync($"Branch used = {branchVer}");
            });
        }
    }
}