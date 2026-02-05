using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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
            AgendaEventResponseDto dto = new AgendaEventResponseDto()
            {
                Id = result.Id,
                Name = result.Name,
                Desc = result.Desc,
                Location = result.Location,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                Category = result.Category.Name
            };

            // Renvoi la réponse sous forme d'un DTO
            return Ok(dto);
        }

        // Endpoint pour ajouter un évent dans la DB
        [HttpPost]
        [ProducesResponseType<AgendaEventResponseDto>(201)]
        public IActionResult AddElement(AgendaEventRequestDto data)
        {
            // Transformer les données "RequestDto" vers le type model (Domain)
            AgendaEvent agendaEvent = new AgendaEvent(
                    data.Name,
                    data.Desc,
                    data.Location,
                    data.StartDate,
                    data.EndDate,
                    new EventCategory(data.Category)
            );

            // Utilisation du service (ApplicationCore) pour ajouter les données
            AgendaEvent result = _agendaEventService.Create(agendaEvent);

            // Transfere des données (Domain) dans un objet "ResponseDTO"
            AgendaEventResponseDto dto = new AgendaEventResponseDto()
            {
                Id = result.Id,
                Name = result.Name,
                Desc = result.Desc,
                Location = result.Location,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                Category = result.Category.Name
            };

            // Création d'une réponse 201 "CREATED"
            return CreatedAtAction(
                nameof(GetById),            // → Endpoint pour récupérer les données
                new { Id = result.Id },     // → Les données necessaire au endpoint (si besoin)
                dto                         // → Les données de l'objet créé
            );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int page, [FromQuery] int nbElement)
        {
            throw new NotImplementedException();
        }

        [HttpGet("date/{startDate}")]
        public IActionResult GetByDate([FromRoute] DateTime startDate)
        {
            throw new NotImplementedException();
        }

        [HttpGet("date/{startDate}/to/{endDate}")]
        public IActionResult GetByDate([FromRoute] DateTime startDate, [FromRoute] DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
