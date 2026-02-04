using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
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
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
//   ...
// - DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    
    // La méthode "GetConnectionString" permet d'obtenir la connection suivante 
    //  - Data Source=ICT-204-00             : Serveur de base de donnée
    //  - Initial Catalog=digital_agenda_db  : La base de donnée ciblée
    //  - Integrated Security=True;          : Login de connexion (Crédential de la machine - Dev only)
    //  - Trust Server Certificate=True      : Validation du certificat auto-signé du serveur (Dev only)
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
