using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.Domain.BusinessExceptions
{
    // Erreur personalisé pour les « AgendaEvent »

    // - Base pour AgendaEvent
    public class AgendaEventException : Exception
    {
        // Donnée de l'event (Pas d'obligation, hein :p)
        public AgendaEvent? AgendaEventData { get; set; }

        // Constructeur
        public AgendaEventException(string message, AgendaEvent? data = null) : base(message)
        {
            AgendaEventData = data;
        }
    }


    // - Spécialiser pour les erreurs lors de la creation
    public class AgendaEventCreateException : AgendaEventException
    {
        private const string INNER_MESSAGE = "Erreur lors de la création de l'événement";

        public AgendaEventCreateException(AgendaEvent data)
            : base(INNER_MESSAGE, data) { }

        public AgendaEventCreateException(string message, AgendaEvent data) 
            : base($"{INNER_MESSAGE} : {message}", data) { }
    }


    // - Spécialiser quand l'élément n'est pas trouvé
    public class AgendaEventNotFoundException : AgendaEventException
    {
        public AgendaEventNotFoundException() : base("L'évenement n'a pas été trouvé !") {}
    }
}
