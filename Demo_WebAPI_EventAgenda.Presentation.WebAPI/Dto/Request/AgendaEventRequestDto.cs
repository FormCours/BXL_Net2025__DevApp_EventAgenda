using System.ComponentModel.DataAnnotations;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request
{
    public class AgendaEventRequestDto
    {
        [Required(AllowEmptyStrings = false)]
        [MinLength(3)]
        [MaxLength(50)]
        public required string Name { get; set; }

        [MinLength(10), MaxLength(2_000)]
        public required string? Desc { get; set; }

        [MaxLength(50)]
        public required string? Location { get; set; }

        [Required]
        public required DateTime StartDate { get; set; }

        public required DateTime? EndDate { get; set; }

        [Required]
        public required string Category { get; set; }
    }
}
