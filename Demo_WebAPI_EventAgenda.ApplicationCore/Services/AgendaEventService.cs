using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.BusinessExceptions;
using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Services
{
    public class AgendaEventService : IAgendaEventService
    {
        private IAgendaEventRepository _agendaEventRepository;
        private IMemberRepository _memberRepository;

        public AgendaEventService(IAgendaEventRepository agendaEventRepository, IMemberRepository memberRepository)
        {
            _agendaEventRepository = agendaEventRepository;
            _memberRepository = memberRepository;
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
            if (page <= 0 || nbElement <= 0)
            {
                throw new ArgumentOutOfRangeException("La page ou le nombre d'élément doivent etre superieur a zéro");
            }

            int offset = (page - 1) * nbElement;
            int limit = nbElement ;
            
            return _agendaEventRepository.GetMany(offset,limit);
        }

        public IEnumerable<AgendaEvent> GetAllByDate(DateTime date)
        {
            return _agendaEventRepository.GetByDate(date);
        }

        public IEnumerable<AgendaEvent> GetAllByDateRange(DateTime startDate, DateTime endDate)
        {
            if(startDate > endDate)
            {
                throw new ArgumentOutOfRangeException("Les dates sont invalides");
            }

            return _agendaEventRepository.GetByDate(startDate, endDate);
        }

        public void UpdateDate(long id, DateTime startDate, DateTime? endDate)
        {
            // Récuperation de l'event
            AgendaEvent? agendaEvent = _agendaEventRepository.GetById(id);

            if(agendaEvent is null)
                throw new AgendaEventNotFoundException();

            // Modification des données via le Domain (Pattern DDD)
            agendaEvent.ChangeDate(startDate, endDate);

            // Répercution du changement dans la base de donnée via le repo
            _agendaEventRepository.Update(agendaEvent);
        }

        public void AddFollower(long agendaEventId, long memberId)
        {
            // Récuperation de l'event
            AgendaEvent? agendaEvent = _agendaEventRepository.GetById(agendaEventId);
            if (agendaEvent is null) throw new AgendaEventNotFoundException();

            // Récuperation de l'utilisateur
            Member? member = _memberRepository.GetById(memberId);
            if (member is null) throw new MemberNotFoundException();

            // Ajout du follower via le répo
            _agendaEventRepository.AddFollower(agendaEventId, memberId);
        }

        public void RemoveFollower(long agendaEventId, long memberId)
        {
            // Récuperation de l'event
            AgendaEvent? agendaEvent = _agendaEventRepository.GetById(agendaEventId);
            if (agendaEvent is null) throw new AgendaEventNotFoundException();

            // Récuperation de l'utilisateur
            Member? member = _memberRepository.GetById(memberId);
            if (member is null) throw new MemberNotFoundException();

            // Suppression du follower via le répo
            _agendaEventRepository.RemoveFollower(agendaEventId, memberId);
        }
    }
}
