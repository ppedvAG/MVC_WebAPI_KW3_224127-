using Microsoft.EntityFrameworkCore;
using WebAPI_with_EFCore_Relationen_LazyLoading.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<GeoDbContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(builder.Configuration.GetConnectionString("GeoDbContext") ?? throw new InvalidOperationException("Connection string 'GeoDbContext' not found."));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
