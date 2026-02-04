using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Repositories
{
    public class AgendaEventRepository : IAgendaEventRepository
    {
        // ↓ Injection (DI) du DbContext dans le Repository (Props + Ctor)
        private readonly AppDbContext _DbContext;

        public AgendaEventRepository(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        // ↓ Implementation du repository
        public AgendaEvent? GetById(long id)
        {
            return _DbContext.AgendaEvents.SingleOrDefault(ae => ae.Id == id);
        }

        public IEnumerable<AgendaEvent> GetMany(int offset, int limit)
        {
            return _DbContext.AgendaEvents
                .AsNoTracking() // (Optimisation) Ne pas tracker les changements des entités récuperés.
                .Skip(offset)   // Permet de ne pas prendre les X premiers
                .Take(limit)    // Permet de selectionné les Y rows
                .ToList();
        }

        public AgendaEvent Insert(AgendaEvent data)
        {
            // Permet d'ajouter dans le context
            EntityEntry<AgendaEvent> element = _DbContext.AgendaEvents.Add(data);

            // Appliquer la modification du context dans la base de donnée
            _DbContext.SaveChanges();

            // Renvoyé l'element ajouté à jours
            return element.Entity;
        }

        public AgendaEvent Update(long id, AgendaEvent data)
        {
            // Dû au pattern "Domain Driven Developpement", on devra coder des méthodes "update" dans le model
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            AgendaEvent? target = GetById(id);

            if(target is null) 
                return false;

            _DbContext.Remove(target);
            _DbContext.SaveChanges();

            return true;
        }
    }
}
