using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Repositories
{
    public class FaqRepository : IFaqRepository
    {

        private readonly AppDbContext _DbContext;

        public FaqRepository(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }


        public IEnumerable<Faq> Get(bool includesHidden, IEnumerable<string> terms)
        {
            IQueryable<Faq> result = _DbContext.Faqs;

            if (!includesHidden)
            {
                result = result.Where(f => f.IsVisible);
            }

            if (terms.Any())
            {
                string[] searchTerms = terms.Where(t => !string.IsNullOrWhiteSpace(t))
                                            .Select(t => t.ToLower())
                                            .ToArray();

                result = result.Where(f => 
                    searchTerms.Any(st => f.Question.ToLower().IndexOf(st) > 0)
                );
            }

            return result.ToList();
        }

        public Faq? GetById(long id)
        {
            return _DbContext.Faqs.SingleOrDefault(f => f.Id == id);
        }

        public Faq Insert(Faq data)
        {
            EntityEntry<Faq> element = _DbContext.Faqs.Add(data);
            _DbContext.SaveChanges();

            return element.Entity;
        }

        public Faq Update(Faq data)
        {
            EntityEntry<Faq> result = _DbContext.Update(data);
            _DbContext.SaveChanges();
            return result.Entity;
        }
    }
}
