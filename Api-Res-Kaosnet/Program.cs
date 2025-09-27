using Api_Res_Kaosnet.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔓 Configura CORS para localhost:5173
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<KaosnetOriginalContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("KaosnetDb"),
    new MySqlServerVersion(new Version(8, 0, 36))));

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔥 Aplica la política CORS
app.UseCors("CorsPolicy");

// ✅ Habilitar archivos estáticos (wwwroot)
app.UseStaticFiles();

// Opcional: carpeta específica de imágenes
var rutaImagenes = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
if (!Directory.Exists(rutaImagenes))
{
    Directory.CreateDirectory(rutaImagenes);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(rutaImagenes),
    RequestPath = "/imagenes"
});

app.UseAuthorization();

app.MapControllers();

app.Run();
