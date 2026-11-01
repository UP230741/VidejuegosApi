using VideojuegosApi.Data;
using VideojuegosApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger para probar los endpoints desde el navegador
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyeccion de dependencias: conexion y repositorio
builder.Services.AddSingleton<Conexion>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

var app = builder.Build();

// Crea la base de datos y la tabla la primera vez que se ejecuta
app.Services.GetRequiredService<Conexion>().InicializarBaseDeDatos();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
