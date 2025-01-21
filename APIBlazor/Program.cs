using APIBlazor.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AsistenciasDbContext>(opciones =>
{
    var connectionString = builder.Configuration.GetConnectionString("conexion");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 2));

    opciones.UseMySql(connectionString, serverVersion);
});

builder.Services.AddCors(opciones => {
    opciones.AddPolicy("nuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("nuevaPolitica");
app.UseAuthorization();

app.MapControllers();

app.Run();
    