using EasyPark.Backend;
using EasyPark.Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de servicios (Inyección de dependencias)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de la conexión a la BD
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection")); //Para configurar la conexión a la base de datos, se puede cambiar "LocalConnection" por el nombre de la cadena de conexión de appsettings.json
builder.Services.AddTransient<SeedDb>();
var app = builder.Build();


SeedData(app);

void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }
}

// 2. Middleware (pipeline de la app)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
