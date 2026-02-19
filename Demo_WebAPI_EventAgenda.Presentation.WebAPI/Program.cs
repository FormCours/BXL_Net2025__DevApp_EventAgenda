using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Utils;
using Demo_WebAPI_EventAgenda.ApplicationCore.Services;
using Demo_WebAPI_EventAgenda.Infrastructure.Database;
using Demo_WebAPI_EventAgenda.Infrastructure.Database.Repositories;
using Demo_WebAPI_EventAgenda.Infrastructure.Mailer;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Configs;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.ExceptionHandlers;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Token;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (→ Injection de dependance)
// Configuration des service possible avec les méthodes suivantes : 
// - AddSingleton : Créer une instance une seul fois et la garde en mémoire.
// - AddScoped : Créer une instance par requete
// - AddTransient : Créer une instance a chaque demande

// DI Configuration 
// - Tools
builder.Services.AddSingleton<TokenTool>();
builder.Services.AddSingleton<IMailerUtil, MailerUtil>(service =>
{
    return new MailerUtil(
        builder.Configuration["Mailer:Host"]!,
        builder.Configuration.GetValue<int>("Mailer:Port", 25),
        builder.Configuration["Mailer:Username"]!,
        builder.Configuration["Mailer:Password"]!,
        builder.Configuration["Mailer:AppEmail"]!,
        builder.Configuration["Mailer:AppName"]!
    );
});

// - Services
builder.Services.AddScoped<IAgendaEventService, AgendaEventService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IFaqService, FaqService>();
//   ...

// - Repository
builder.Services.AddScoped<IAgendaEventRepository, AgendaEventRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IFaqRepository, FaqRepository>();
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

// Mapping des controllers
builder.Services.AddControllers();

// Gestion des exceptions (Pattern "Exception Handler")
builder.Services.AddExceptionHandler<AgendaEventExceptionHandler>();
builder.Services.AddProblemDetails();

// Configuration de l'authentification par JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    byte[] secretKey = Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"]!);

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // ↓ Valeur valide pour la config du token
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        ValidAudience = builder.Configuration["Token:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                        // ↓ Regles de validation 
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                    };
                });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// https://learn.microsoft.com/fr-fr/aspnet/core/fundamentals/openapi/customize-openapi?view=aspnetcore-10.0
builder.Services.AddOpenApi(options =>
{

    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "Agenda Event API",
            Version = "v1",
            Description = "Démo d'une API RESTFull pour le groupe .Net React de DigitalCity"
        };
        return Task.CompletedTask;
    });
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();

});

// ----------------------------------------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();  // ← Active le systeme d'authenfication précédement configuré !!!
app.UseAuthorization();

app.MapControllers();

app.Run();
