using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response
{
    // Le dto pour quand on renvoi un élément
    public class AgendaEventResponseDto
    {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public required string? Desc { get; set; }
        public required string? Location { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime? EndDate { get; set; }
        public required string Category { get; set; }
    }

    // Le dto pour quand on renvoi une liste d'élément
    public class AgendaEventListItemResponseDto
    {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime? EndDate { get; set; }
    }
}
