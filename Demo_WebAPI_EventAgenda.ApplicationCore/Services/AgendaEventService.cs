using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.BusinessExceptions;
using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Services
{
    public class AgendaEventService : IAgendaEventService
    {
        private IAgendaEventRepository _agendaEventRepository;

        public AgendaEventService(IAgendaEventRepository agendaEventRepository)
        {
            _agendaEventRepository = agendaEventRepository;
        }


        public AgendaEvent Create(AgendaEvent data)
        {
            // Erreur si la date de debut d'évent est plus petit que la date de demain
            if (data.StartDate < DateTime.Today.AddDays(1))
            {
                // Générer une erreur liée au régle métier !
                throw new AgendaEventCreateException(data);
            }

            return _agendaEventRepository.Insert(data);
        }

        public void Delete(long id)
        {
            bool success = _agendaEventRepository.Delete(id);
            if (!success)
            {
                throw new AgendaEventNotFoundException();
            }
        }

        public AgendaEvent GetById(long id)
        {
            AgendaEvent? data = _agendaEventRepository.GetById(id);

            if (data is null)
            {
                throw new AgendaEventNotFoundException();
            }
            return data;


            // return _agendaEventRepository.GetById(id) ?? throw new AgendaEventNotFoundException();
        }

        public IEnumerable<AgendaEvent> GetMany(int page, int nbElement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AgendaEvent> GetAllByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AgendaEvent> GetAllByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public void UpdateDate(long id, DateTime startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }
    }
}
