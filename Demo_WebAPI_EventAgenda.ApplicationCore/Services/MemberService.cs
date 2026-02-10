using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Soenneker.Hashing.Argon2;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }


        public Member Register(Member member)
        {
            if(string.IsNullOrEmpty(member.HashPwd))
            {
                throw new ArgumentNullException("Password non défini !");
            }

            // Hashage du mot de passe
            // (La méthode est asynchrone, pour simplification, on va exploiter sans le coté async)
            string hashPwd = Argon2HashingUtil.Hash(member.HashPwd).Result;

            // Re-création de l'objet member avec le mot de passe hashé
            Member memberToInsert = new Member(
                member.Email,
                member.Pseudo,
                member.AllowNewsletter,
                hashPwd
            );

            // Créer le compte dans la base de donnée via le repo
            return _memberRepository.Insert(memberToInsert);
        }
    }
}
