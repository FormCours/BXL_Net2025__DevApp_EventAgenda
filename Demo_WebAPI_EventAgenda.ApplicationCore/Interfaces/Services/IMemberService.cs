using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services
{
    public interface IMemberService
    {
        Member Register(Member member);
        Member Login(string email, string password);
    }
}
