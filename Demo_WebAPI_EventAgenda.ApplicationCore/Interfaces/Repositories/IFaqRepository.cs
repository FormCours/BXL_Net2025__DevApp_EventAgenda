using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories
{
    public interface IFaqRepository
    {
        IEnumerable<Faq> Get(bool includesHidden, IEnumerable<string> terms);
        Faq? GetById(long id);
        Faq Insert(Faq data);
        Faq Update(Faq faq);
    }
}
