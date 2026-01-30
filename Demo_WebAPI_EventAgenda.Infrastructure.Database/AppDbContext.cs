using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Infrastructure.Database.Configs;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database
{
    // Configuration de la base de donnée pour EFCore
    public class AppDbContext : DbContext
    {

        // Ensemble des tables
        public DbSet<AgendaEvent> AgendaEvents { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }


        // Définition du ctor (Utiliser par l'injection de dépendance)
        public AppDbContext(DbContextOptions options) : base(options) { }


        // Appliquer de la configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Charger des classes qui implement "IEntityTypeConfiguration<>"

            // - Ajout de toutes les configs à la mano
            /*
            modelBuilder.ApplyConfiguration(new AgendaEventConfig());
            modelBuilder.ApplyConfiguration(new EventCategoryConfig());
            */

            // - Ajout automatique des configs de l'assemble courante
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
