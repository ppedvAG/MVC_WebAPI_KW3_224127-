using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using WebAPI_with_Autofac.Interface;
using WebAPI_with_Autofac.Implementation;

CreateWebHostBuilder(args).Build().Run();


static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();





public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {

        services.AddControllers().AddControllersAsServices();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoFacImplementationWeb", Version = "v1" });
        });

        ContainerBuilder builder = new ContainerBuilder();

        //ServiceCollection initialisiert den ContainerBuilder. 
        builder.Populate(services);
       
        //Weitere Services können wir auch via AutoFac Manier an den IOC Container hängen
        builder.RegisterType<PersonBusiness>().As<IPersonBusiness>();
        builder.RegisterType<StringBusiness>().As<IStringBusiness>();

        var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();

        builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();

        return new AutofacServiceProvider(builder.Build());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoFacImplementationWeb v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

//WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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
