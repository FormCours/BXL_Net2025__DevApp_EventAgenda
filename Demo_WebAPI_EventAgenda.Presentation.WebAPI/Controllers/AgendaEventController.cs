using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet(":id")]
        public IActionResult GetById([FromRoute] long id)
        {
            AgendaEvent result = _agendaEventService.GetById(id);

            // TODO Ne pas envoyer l'objet du Domain, passer par un DTO !

            return Ok(result);
        }
    }
}
