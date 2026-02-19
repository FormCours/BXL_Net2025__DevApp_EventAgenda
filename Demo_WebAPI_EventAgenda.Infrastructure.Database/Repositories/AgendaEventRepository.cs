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
            return _DbContext.AgendaEvents
                .Include(ae => ae.Category)
                .SingleOrDefault(ae => ae.Id == id);
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
            // Check de l'existance des catégories
            EventCategory? categoryInDB = _DbContext.EventCategories.SingleOrDefault(c => c.Name == data.Category.Name);

            // Ré-créer l'élément à ajouter en DB avec le lien vers la categorie si elle existe (Limitation du au DDD)
            AgendaEvent dataToInsert = new AgendaEvent(
                data.Name,
                data.Desc,
                data.Location,
                data.StartDate,
                data.EndDate,
                categoryInDB ?? data.Category  // Coalesce -> Categorie existante sinon la categorie demandé.
            );

            // Permet d'ajouter dans le context
            EntityEntry<AgendaEvent> element = _DbContext.AgendaEvents.Add(dataToInsert);

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
            // Cleanup les parametres d'entrées
            // -> Le debut est mis à 0h 00m 00s
            // -> La fin est mise à 23h 59m 59.9999s
            DateTime searchStartDate = startDate.Date;
            DateTime searchEndDate = (endDate ?? startDate).Date.AddDays(1).AddTicks(-1);

            var result = _DbContext.AgendaEvents
                            .AsNoTracking()
                            .Where(ae => // Linq to EF => La condition sera executer en SQL !!!!
                                (
                                    // Si l'event commence avant la recherche, on vérifie que la fin est après le debut chercher
                                    ae.StartDate <= searchStartDate
                                    &&
                                    (ae.EndDate ?? ae.StartDate) >= searchStartDate
                                )
                                ||
                                (
                                    // Si l'event termine aprés la recherche, on vérifie que la debut est avant la fin chercher
                                    (ae.EndDate ?? ae.StartDate) >= searchEndDate
                                    &&
                                    ae.StartDate <= searchEndDate
                                )
                                ||
                                (
                                    // L'event est compris dans la recherche
                                    ae.StartDate >= searchStartDate
                                    &&
                                    (ae.EndDate ?? ae.StartDate) <= searchEndDate
                                )
                            ).ToList();

            return result;
        }

        public void AddFollower(long agendaEventId, long memberId)
        {
            // TODO Finish this
        }

        public void RemoveFollower(long agendaEventId, long memberId)
        {
            // TODO Finish this
        }

    }
}



/* 
Exemple d'event pour la méthode "GetByDate"
- Event
    10/02           A
    20/02           B
    07/02  10/02    C
    15/02  25/02    D
    01/09  20/10    E

- Recherche
    10/02  -----    => A C
    22/02  -----    => D
    09/02  19/02    => A C D
*/