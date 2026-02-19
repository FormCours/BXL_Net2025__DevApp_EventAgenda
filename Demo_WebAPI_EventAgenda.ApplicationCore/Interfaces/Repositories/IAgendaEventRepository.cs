using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories
{
    public interface IAgendaEventRepository
    {
        AgendaEvent? GetById(long id);
        IEnumerable<AgendaEvent> GetMany(int offset, int limit);

        AgendaEvent Insert(AgendaEvent data);
        AgendaEvent Update(AgendaEvent data);
        bool Delete(long id);

        IEnumerable <AgendaEvent> GetByDate (DateTime startDate, DateTime? endDate = null);
        void AddFollower(long agendaEventId, long memberId);
        void RemoveFollower(long agendaEventId, long memberId);
    }
}
