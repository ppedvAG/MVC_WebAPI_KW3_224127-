using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<ContainerBuilder>(autofacBuilder =>
{
    //ServiceCollectionExtiontions.ConfigureServiceCollection(builder.Services);


    //autofacBuilder.Populate(builder.Services);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoFacImplementationWeb v1"));
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();


public static class ServiceCollectionExtiontions
{
    public static void ConfigureServiceCollection(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}