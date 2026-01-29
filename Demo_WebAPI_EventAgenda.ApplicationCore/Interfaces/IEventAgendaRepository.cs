using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces
{
    public interface IEventAgendaRepository
    {
        AgendaEvent GetById(long id);
        IEnumerable<AgendaEvent> GetMany(int offset, int limit);

        AgendaEvent Insert(AgendaEvent data);
        AgendaEvent Update(long id, AgendaEvent data);
        bool Delete(long id);
    }
}
