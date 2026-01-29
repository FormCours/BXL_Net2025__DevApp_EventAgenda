using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database
{
    // Configuration de la base de donnée pour EFCore
    public class AppDbContext : DbContext
    {
        // Ensemble des tables
        public DbSet<AgendaEvent> AgendaEvents { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }



    }
}
