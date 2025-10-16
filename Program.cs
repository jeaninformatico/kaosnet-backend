using KaosNetApi.Data;
using Microsoft.EntityFrameworkCore;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Services;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno
builder.Configuration.AddEnvironmentVariables();

// Configurar URL de escucha
builder.WebHost.UseUrls(builder.Configuration["ASPNETCORE_URLS"] ?? "http://+:5030");

// Obtener cadena de conexión desde variable de entorno
var connectionString = builder.Configuration["ConnectionStrings:KaosNetDb"];
// Registrar DbContext con MySQL/MariaDB
builder.Services.AddDbContext<KaosNetDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Registrar servicios
builder.Services.AddScoped<IRoleService, RoleService>();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Activar Swagger como página principal
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "KaosNet API V1");
    c.RoutePrefix = "";
});

// Redirección HTTPS (opcional en Docker)
app.UseHttpsRedirection();

// Endpoint de prueba de conexión a la base de datos
app.MapGet("/test-db", async (KaosNetDbContext db) =>
{
    try
    {
        var canConnect = await db.Database.CanConnectAsync();
        return canConnect
            ? Results.Ok(new { message = "✅ Conectado correctamente a la base de datos." })
            : Results.Problem("❌ No se pudo conectar a la base de datos.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error: {ex.Message}");
    }
})
.WithName("TestDatabase")
.WithOpenApi();

// Endpoint de ejemplo: pronóstico del clima
app.MapGet("/weatherforecast", () =>
{
    var summaries = new[]
    {
        "Freezing","Bracing","Chilly","Cool","Mild",
        "Warm","Balmy","Hot","Sweltering","Scorching"
    };
    var forecast = Enumerable.Range(1,5).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20,55),
            summaries[Random.Shared.Next(summaries.Length)]
        )
    ).ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// Mapear controladores
app.MapControllers();
app.Run();

// Registro simple para el endpoint de clima
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
