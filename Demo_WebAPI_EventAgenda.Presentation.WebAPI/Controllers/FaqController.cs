using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Mappers;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FaqController : ControllerBase
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_faqService.GetAll().Select(FaqMapper.ToResponse));
        }

        [HttpGet("search")]
        public IActionResult GetSearch([FromQuery] string[] terms)
        {
            return Ok(_faqService.GetBySearch(terms).Select(FaqMapper.ToResponse));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddElement([FromBody] FaqRequestDto dto) {

            Faq faq = _faqService.Create(dto.ToDomain());

            return CreatedAtAction(
                nameof(GetAll),
                faq.ToResponse()
            );
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateElement([FromRoute]long id, [FromBody] FaqRequestPatchDto dto)
        {
            if(dto.Visibility is not null)
            {
                _faqService.UpdateVisibility(id, (bool)dto.Visibility);
            }

            return Accepted();
        }


        [HttpPost("{id}/like")]
        public IActionResult LikeElement(long id)
        {
            _faqService.AddLike(id);
            return Accepted();
        }
    }
}
