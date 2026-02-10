using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.ApplicationCore.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Dto.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public AuthController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] AuthRegisterRequestDto dto)
        {
            Member member = new Member(
                dto.Email,
                dto.Pseudo,
                dto.AllowNewsletter,
                dto.Password
            ); 

            _memberService.Register(member);

            return Ok(new
            {
                Message = "Votre compte a bien été créé !"
            });
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] AuthLoginRequestDto dto)
        {
            Member member = _memberService.Login(dto.Email, dto.Password);


            return Ok(new
            {
                Message = "Bravo, vous avez mit des credentials valides 👈(ﾟヮﾟ👈)"
            });
        }
    }
}
