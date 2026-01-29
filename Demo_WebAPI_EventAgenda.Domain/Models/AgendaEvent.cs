namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class AgendaEvent
    {
        // Propriétés
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string? Location { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public EventCategory Category { get; private set; }

        // Constructeur
        public AgendaEvent() { }

        public AgendaEvent(string name, string? location, DateTime startDate, DateTime? endDate, EventCategory category)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Le nom de l'évenement doit contenir au moins un caractère", nameof(name));

            if (endDate is not null && endDate < startDate)
                throw new ArgumentException("Les dates de l'événement sont invalides");

            Name = name;
            Location = location;
            StartDate = startDate;
            EndDate = endDate;
            Category = category;
        }
    }
}
