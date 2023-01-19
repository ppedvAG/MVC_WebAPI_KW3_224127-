using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using ProtoBuf.Meta;
using System.Reflection;
using WebAPI_ControllerSamples.Data;
using WebApiContrib.Core.Formatter.Csv;
using WebApiContrib.Core.Formatter.Protobuf;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDbContext") ?? throw new InvalidOperationException("Connection string 'MovieDbContext' not found.")));

// Add services to the container.


builder.Services.AddControllers(options=>
{
    options.FormatterMappings
        .SetMediaTypeMappingForFormat("protobuf",
          MediaTypeHeaderValue.Parse("application/x-protobuf"));
})
    .AddXmlSerializerFormatters()
    .AddCsvSerializerFormatters()
    .AddProtobufFormatters();


builder.Services.AddResponseCaching();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
