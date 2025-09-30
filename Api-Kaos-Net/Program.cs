using Api_Kaos_Net.Data;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Services; // Asegúrate de tener este using
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔌 Configuración de la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = ServerVersion.Parse("11.4.7-mariadb");

// 🧱 Inyección del DbContext con MariaDB
builder.Services.AddDbContext<KaosnetDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// 🧩 Registro del servicio para Role
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleAccessService, RoleAccessService>();
builder.Services.AddScoped<IViewService, ViewService>();
builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService>();
builder.Services.AddScoped<ISubscriptionPlanAccountService, SubscriptionPlanAccountService>();
builder.Services.AddScoped<IStreamingTypeService, StreamingTypeService>();
builder.Services.AddScoped<IStreamingAccountService, StreamingAccountService>();
builder.Services.AddScoped<ISalesAccountService, SalesAccountService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IConversionRateService, ConversionRateService>();






// Add services to the container.
builder.Services.AddControllers();
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
