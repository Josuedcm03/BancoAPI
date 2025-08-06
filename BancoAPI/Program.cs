using Microsoft.EntityFrameworkCore;
using BancoAPI.Services;
using BancoAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Registrar el DbContext y configurar SQLite
builder.Services.AddDbContext<BancoDbContext>(options =>
    options.UseSqlite("Data Source=banco.db"));

// Registro de los servicios 
builder.Services.AddScoped<IClienteService, ClienteService>(); // Registrar el servicio de cliente, indicandole al framework que use ClienteService para IClienteService

// Registrar el servicio de cuenta, indicandole al framework que use CuentaService para ICuentaService
builder.Services.AddScoped<ICuentaService, CuentaService>();

// Registrar el servicio de transacción, indicandole al framework que use TransaccionService para ITransaccionService
builder.Services.AddScoped<ITransaccionService, TransaccionService>();



builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Agregar soporte para controladores y documentación Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapea automáticamente todos los controladores
app.MapControllers();

app.Run();
