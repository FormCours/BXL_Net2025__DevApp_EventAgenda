using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _DbContext;

        public MemberRepository(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }


        public Member Insert(Member data)
        {
            EntityEntry<Member> element = _DbContext.Members.Add(data);
            _DbContext.SaveChanges();

            var result = element.Entity;
            return new Member(result.Id, result.Email, result.Pseudo, result.AllowNewsletter);
        }

        public string? GetHashPwd(string email)
        {
            return _DbContext.Members.SingleOrDefault(m => m.Email == email)?.HashPwd;
        }
    }
}
