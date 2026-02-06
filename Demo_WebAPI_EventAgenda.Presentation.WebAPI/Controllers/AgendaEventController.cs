using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Mappers;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Xml.Linq;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaEventController : ControllerBase
    {
        // La dépendence vers le service (ApplicationCore)
        private readonly IAgendaEventService _agendaEventService;

        // Injection de dépendence via le constructeur
        public AgendaEventController(IAgendaEventService agendaEventService)
        {
            _agendaEventService = agendaEventService;
        }


        // Endpoint pour récuperer un évent de la DB via son Id
        [HttpGet("{id}")]
        [ProducesResponseType<AgendaEventResponseDto>(200)]
        public IActionResult GetById([FromRoute] long id)
        {
            // Récuperation des données depuis le service "ApplicationCore"
            AgendaEvent result = _agendaEventService.GetById(id);

            // Transfere des données (Domain) dans un objet "ResponseDTO"
            AgendaEventResponseDto dto = result.ToResponseDto();

            // Renvoi la réponse sous forme d'un DTO
            return Ok(dto);
        }


        // Endpoint pour ajouter un évent dans la DB
        [HttpPost]
        [ProducesResponseType<AgendaEventResponseDto>(201)]
        public IActionResult AddElement(AgendaEventRequestDto data)
        {
            // Transformer les données "RequestDto" vers le type model (Domain)
            AgendaEvent agendaEvent = data.ToDomain();

            // Utilisation du service (ApplicationCore) pour ajouter les données
            AgendaEvent result = _agendaEventService.Create(agendaEvent);

            // Transfere des données (Domain) dans un objet "ResponseDTO"
            AgendaEventResponseDto dto = result.ToResponseDto();


            // ↓ Version simplifier avec les mappers
            // AgendaEventResponseDto dto2 = _agendaEventService.Create(data.ToDomain()).ToResponseDto();

            // Création d'une réponse 201 "CREATED"
            return CreatedAtAction(
                nameof(GetById),            // → Endpoint pour récupérer les données
                new { Id = result.Id },     // → Les données necessaire au endpoint (si besoin)
                dto                         // → Les données de l'objet créé
            );
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public IActionResult Delete(int id)
        {
            _agendaEventService.Delete(id);
            return NoContent();
        }


        [HttpGet]
        [ProducesResponseType<IEnumerable<AgendaEventListItemResponseDto>>(200)]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int nbElement = 10)
        {
            IEnumerable<AgendaEvent> result = _agendaEventService.GetMany(page, nbElement);

            IEnumerable<AgendaEventListItemResponseDto> dto = result.Select(item => item.ToListResponseDto());

            return Ok(dto);
        }


        [HttpGet("date/{startDate}")]
        [ProducesResponseType<IEnumerable<AgendaEventListItemResponseDto>>(200)]
        public IActionResult GetByDate([FromRoute] DateTime startDate)
        {
            IEnumerable<AgendaEvent> result = _agendaEventService.GetAllByDate(startDate);

            IEnumerable<AgendaEventListItemResponseDto> dto = result.Select(AgendaEventMapper.ToListResponseDto);

            return Ok(dto);
        }



        [HttpGet("date/{startDate}/to/{endDate}")]
        [ProducesResponseType<IEnumerable<AgendaEventListItemResponseDto>>(200)]
        public IActionResult GetByDate([FromRoute] DateTime startDate, [FromRoute] DateTime endDate)
        {
            IEnumerable<AgendaEvent> result = _agendaEventService.GetAllByDateRange(startDate, endDate);

            IEnumerable<AgendaEventListItemResponseDto> dto = result.Select(AgendaEventMapper.ToListResponseDto);

            return Ok(dto);
        }
    }
}
