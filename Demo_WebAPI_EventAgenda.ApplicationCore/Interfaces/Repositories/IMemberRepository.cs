using Demo_WebAPI_EventAgenda.Domain.Models;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories
{
    public interface IMemberRepository
    {
        Member Insert(Member data);
        string? GetHashPwd(string email);
    }
}
