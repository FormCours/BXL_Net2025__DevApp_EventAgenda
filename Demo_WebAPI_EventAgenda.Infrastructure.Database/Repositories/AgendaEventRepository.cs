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

        public AgendaEvent Update(AgendaEvent data)
        {
            // Permet de modifier dans l'objet du context
            EntityEntry<AgendaEvent> result = _DbContext.Update(data);

            // Appliquer la modification du context dans la base de donnée
            _DbContext.SaveChanges();

            // Renvoyé l'element ajouté à jours
            return result.Entity;
        }

        public bool Delete(long id)
        {
            AgendaEvent? target = GetById(id);

            if (target is null)
                return false;

            _DbContext.Remove(target);
            _DbContext.SaveChanges();

            return true;
        }

        public IEnumerable<AgendaEvent> GetByDate(DateTime startDate, DateTime? endDate = null)
        {
            DateTime currentEndDate = endDate ?? startDate;

            var result = _DbContext.AgendaEvents
                            .AsNoTracking()
                            .Where(ae => ae.StartDate <= currentEndDate || ae.EndDate >= startDate)
                            .ToList();

            return result;
        }
    }
}



/* 
Exemple d'event pour la méthode "GetByDate"
- Event
    10/02           A
    20/02           B
    05/02  10/02    C
    15/02  25/02    D

- Recherche
    10/02  -----    => A C
    22/02  -----    => D
    09/02  19/02    => A C D
*/