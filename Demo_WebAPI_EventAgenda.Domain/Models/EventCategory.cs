namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class EventCategory
    {
        // Propriétés
        public long Id { get; private set; }
        public string Name { get; private set; }

        // Constructeur
        // - Vide → Necessaire pour EntityFramework
        public EventCategory() { }

        // - Parametres → Avec validation
        public EventCategory(string name)
        {
            if (name.Trim().Length < 3) 
                throw new ArgumentException("Le nom de la catégorie doit contenir au moins 3 caractères", nameof(name));

            Name = name.Trim();
        }
    }
}
