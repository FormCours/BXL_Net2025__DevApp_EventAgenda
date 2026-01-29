using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces;
using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Repositories
{
    public class AgendaEventRepository : IAgendaEventRepository
    {
        public AgendaEvent GetById(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AgendaEvent> GetMany(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public AgendaEvent Insert(AgendaEvent data)
        {
            throw new NotImplementedException();
        }

        public AgendaEvent Update(long id, AgendaEvent data)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
