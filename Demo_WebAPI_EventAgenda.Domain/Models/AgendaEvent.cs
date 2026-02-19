using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class AgendaEvent 
    {
        // Propriétés
        public long Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string? Desc { get; private set; }
        public string? Location { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public EventCategory Category { get; private set; } = default!;
        public List<Member> Followers { get; private set; } = [];

        // Constructeur
        private AgendaEvent() { }

        public AgendaEvent(string name, string? desc, string? location, DateTime startDate, DateTime? endDate, EventCategory category)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Le nom de l'évenement doit contenir au moins un caractère", nameof(name));

            if (endDate is not null && endDate < startDate)
                throw new ArgumentException("Les dates de l'événement sont invalides");

            Name = name;
            Desc = desc;
            Location = location;
            StartDate = startDate;
            EndDate = endDate;
            Category = category;
        }

        public AgendaEvent ChangeDate(DateTime startDateUpdated, DateTime? endDateUpdated)
        {
            if(endDateUpdated is not null && endDateUpdated < startDateUpdated)
                throw new ArgumentException("Les dates de l'événement sont invalides");

            StartDate = startDateUpdated;
            EndDate = endDateUpdated;

            return this;  // -> Permet d'enchainer les méthodes (C'est optionnel)
        }
    }
}
