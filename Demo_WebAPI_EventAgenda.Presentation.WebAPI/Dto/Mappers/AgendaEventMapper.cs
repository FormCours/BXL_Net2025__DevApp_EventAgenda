using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Mappers
{
    // ↓ Classe qui contiendra des méthodes d'extension pour réaliser le mapping
    public static class AgendaEventMapper
    {
        // Mapper pour convertir le modele (Domain) vers le "ResponseDto" (Présentation)
        public static AgendaEventResponseDto ToResponseDto(this AgendaEvent data)
        {
            return new AgendaEventResponseDto()
            {
                Id = data.Id,
                Name = data.Name,
                Desc = data.Desc,
                Location = data.Location,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Category = data.Category.Name
            };
        }

        public static AgendaEventListItemResponseDto ToListResponseDto(this AgendaEvent data)
        {
            return new AgendaEventListItemResponseDto()
            {
                Id = data.Id,
                Name = data.Name,
                StartDate = data.StartDate,
                EndDate = data.EndDate
            };
        }


        // Mapper pour convertir le "RequestDto" (Présentation) vers le modele (Domain)
        public static AgendaEvent ToDomain(this AgendaEventRequestDto dto)
        {
            return new AgendaEvent(
                    dto.Name,
                    dto.Desc,
                    dto.Location,
                    dto.StartDate,
                    dto.EndDate,
                    new EventCategory(dto.Category)
            );
        }

    }
}
