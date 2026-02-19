using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services
{
    public interface IAgendaEventService
    {
        AgendaEvent GetById(long id);

        IEnumerable<AgendaEvent> GetMany(int page, int nbElement);
        IEnumerable<AgendaEvent> GetAllByDate(DateTime date);
        IEnumerable<AgendaEvent> GetAllByDateRange(DateTime startDate, DateTime endDate);

        AgendaEvent Create(AgendaEvent data);
        void UpdateDate(long id, DateTime startDate, DateTime? endDate = null);
        void Delete(long id);

        void AddFollower(long agendaEventId, long memberId);
        void RemoveFollower(long agendaEventId, long memberId);
    }
}
