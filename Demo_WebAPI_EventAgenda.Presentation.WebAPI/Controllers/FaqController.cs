using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Mappers;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }


        [HttpGet]
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
        public IActionResult AddElement([FromBody] FaqRequestDto dto) {

            Faq faq = _faqService.Create(dto.ToDomain());

            return CreatedAtAction(
                nameof(GetAll),
                faq.ToResponse()
            );
        }

        [HttpPatch("{id}/show")]
        public IActionResult ShowElement(long id)
        {
            _faqService.UpdateVisibility(id, true);
            return NoContent();
        }

        [HttpPatch("{id}/hide")]
        public IActionResult HideElement(long id)
        {
            _faqService.UpdateVisibility(id, false);
            return NoContent();
        }
    }
}
