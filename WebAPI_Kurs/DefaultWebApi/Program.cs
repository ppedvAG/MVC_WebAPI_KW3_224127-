//ASPNET Core verwendet generell den WebApplicationBuilder
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//AddControllers wird für die WebAPI verwendet
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Dokumentations und Testumgebung für die WebAPI
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Swagger sollte auch nur auf Entwicklerrechner laufen -> Oder wir verwenden eine Public-WebAPI für Jedermann
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Endpunkt in der Middleware + Request Navigator -> Request findet Controller + Methode
app.MapControllers();

app.Run();
