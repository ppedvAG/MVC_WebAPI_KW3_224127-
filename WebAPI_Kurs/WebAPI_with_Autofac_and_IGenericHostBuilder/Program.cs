
// ASP.NET Core 3.0+:
// The UseServiceProviderFactory call attaches the
// Autofac provider to the generic hosting mechanism.
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;

var host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureWebHostDefaults(webHostBuilder => {
        webHostBuilder
              .UseStartup<Startup>();
    })
    .Build();

host.Run();



public class Startup
{
    public Startup(IConfiguration configuration)
    {
        // In ASP.NET Core 3.x, using `Host.CreateDefaultBuilder` (as in the preceding Program.cs snippet) will
        // set up some configuration for you based on your appsettings.json and environment variables. See "Remarks" at
        // https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.host.createdefaultbuilder for details.
        this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; private set; }

    public ILifetimeScope AutofacContainer { get; private set; }

    // ConfigureServices is where you register dependencies. This gets
    // called by the runtime before the ConfigureContainer method, below.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the collection. Don't build or return
        // any IServiceProvider or the ConfigureContainer method
        // won't get called. Don't create a ContainerBuilder
        // for Autofac here, and don't call builder.Populate() - that
        // happens in the AutofacServiceProviderFactory for you.
        services.AddOptions();
        services.AddControllers().AddControllersAsServices();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoFacImplementationWeb", Version = "v1" });
        });
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
    public void ConfigureContainer(ContainerBuilder builder)
    {
        // Register your own things directly with Autofac here. Don't
        // call builder.Populate(), that happens in AutofacServiceProviderFactory
        // for you.


        //builder.RegisterModule(new MyApplicationModule());


    }

    // Configure is where you add middleware. This is called after
    // ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
    // here if you need to resolve things from the container.
    public void Configure(
      IApplicationBuilder app,
      ILoggerFactory loggerFactory)
    {
        // If, for some reason, you need a reference to the built container, you
        // can use the convenience extension method GetAutofacRoot.
        this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

        //loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
        //loggerFactory.AddDebug();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}








//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
