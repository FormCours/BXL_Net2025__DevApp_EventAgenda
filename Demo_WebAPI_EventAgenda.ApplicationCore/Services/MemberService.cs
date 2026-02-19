using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Utils;
using Demo_WebAPI_EventAgenda.Domain.BusinessExceptions;
using Demo_WebAPI_EventAgenda.Domain.Models;
using Soenneker.Hashing.Argon2;

namespace Demo_WebAPI_EventAgenda.ApplicationCore.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMailerUtil _mailerUtil;

        public MemberService(IMemberRepository memberRepository, IMailerUtil mailerUtil)
        {
            _memberRepository = memberRepository;
            _mailerUtil = mailerUtil;
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
            Member memberInserted = _memberRepository.Insert(memberToInsert);

            // Envoi du mail de bienvenue
            _mailerUtil.SendWelcomeMessage(member);

            // Envoi du compte créer
            return memberInserted;
        }

        public Member Login(string email, string password)
        {
            string? hashPwd = _memberRepository.GetHashPwd(email);
            if(hashPwd is null)
            {
                // Le compte n'existe pas !
                throw new MemberBadCredentialException();
            }

            bool isValid = Argon2HashingUtil.Verify(password, hashPwd).Result;
            if (!isValid) 
            {
                // Le mot de passe est invalide !
                throw new MemberBadCredentialException();
            }

            return _memberRepository.GetByEmail(email);
        }
    }
}
