using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Mappers
{
    public static class FaqMapper
    {
        public static FaqResponseDto ToResponse(this Faq data)
        {
            return new FaqResponseDto()
            {
                Id = data.Id,
                Question = data.Question,
                Response = data.Response,
                IsHidden = !data.IsVisible,
                NbLike = data.NbLike,
            };
        }

        public static Faq ToDomain(this FaqRequestDto requestDto)
        {
            return new Faq(
                requestDto.Question,
                requestDto.Response
            );
        }
    }
}
