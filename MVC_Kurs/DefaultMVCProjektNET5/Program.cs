using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultMVCProjektNET5
{
    public class Program
    {
        public static void Main(string[] args)
        {


            //.NET 5 Standard-Implementierung
            CreateHostBuilder(args).Build().Run();


            //.NET 7 kann auch auf IHostBuilder zugreifen
            //WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            //builder.HostBuilder = CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
