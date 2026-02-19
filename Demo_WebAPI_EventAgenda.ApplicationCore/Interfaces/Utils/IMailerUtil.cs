using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Utils
{
    public interface IMailerUtil
    {
        void SendWelcomeMessage(Member member);
    }
}
