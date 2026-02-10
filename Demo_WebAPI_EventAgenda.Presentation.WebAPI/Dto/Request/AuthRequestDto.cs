using System.ComponentModel.DataAnnotations;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request
{
    public class AuthRegisterRequestDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public required string Email { get; set; }

        [MinLength(5)]
        [MaxLength(50)]
        public required string? Pseudo { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression("(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).*")]
        public required string Password { get; set; }

        [Required]
        public required bool AllowNewsletter { get; set; }
    }

    public class AuthLoginRequestDto
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
