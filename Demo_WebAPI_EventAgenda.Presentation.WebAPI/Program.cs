using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces;
using Demo_WebAPI_EventAgenda.Infrastructure.Database;
using Demo_WebAPI_EventAgenda.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (→ Injection de dependance)
// Configuration des service possible avec les méthodes suivantes : 
// - AddSingleton : Créer une instance une seul fois et la garde en mémoire.
// - AddScoped : Créer une instance par requete
// - AddTransient : Créer une instance a chaque demande

// DI Configuration 
// - Services (TODO)
//   ...
// - Repository
builder.Services.AddScoped<IAgendaEventRepository, AgendaEventRepository>();
//   ...
// - DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("Connection sera présente ici apres la pause 🍔");
});



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
