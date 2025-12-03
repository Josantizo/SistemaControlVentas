using Microsoft.EntityFrameworkCore;
using SistemaControlVentas.Data;
using SistemaControlVentas.Repositories;
using SistemaControlVentas.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar Entity Framework y MySQL
// La contraseña puede venir de:
// 1. Variable de entorno: ConnectionStrings__DefaultConnection (doble guión bajo)
// 2. Variable de entorno: DB_PASSWORD (solo la contraseña)
// 3. appsettings.json (NO incluir contraseñas en producción)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

// Si la contraseña viene de variable de entorno DB_PASSWORD, reemplazarla
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
if (!string.IsNullOrEmpty(dbPassword))
{
    // Reemplazar o agregar la contraseña en la cadena de conexión
    if (connectionString.Contains("Password="))
    {
        connectionString = System.Text.RegularExpressions.Regex.Replace(
            connectionString, 
            @"Password=[^;]*", 
            $"Password={dbPassword}");
    }
    else
    {
        connectionString = connectionString.TrimEnd(';') + $";Password={dbPassword};";
    }
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configurar Repositorios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();

// Configurar Servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IReporteExcelService, ReporteExcelService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
