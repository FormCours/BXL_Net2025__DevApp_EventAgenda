using Demo_WebAPI_EventAgenda.Domain.Enums;
using System.Net.Mail;

namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class Member 
    {
        // Propriétés
        public long Id { get; private set; }
        public string Email { get; private set; } = default!;
        public string? Pseudo { get; private set; }
        public string? HashPwd { get; private set; }
        public bool AllowNewsletter { get; private set; }
        public MemberRoleEnum Role { get; private set; }
        public List<AgendaEvent> FollowEvents { get; private set; } = [];

        // Constructeur
        private Member() { }

        public Member(string email, string? pseudo, bool allowNewsletter, string? hashPwd = null)
        {
            if (string.IsNullOrWhiteSpace(email) || !MailAddress.TryCreate(email, out _))
                throw new ArgumentException("L'adresse email n'est pas valide", nameof(email));

            if(Pseudo is not null && (Pseudo.Trim().Length < 3 || Pseudo.Trim().Length > 50))
                throw new ArgumentException("Le pseudonyme n'est pas valide", nameof(pseudo));

            Email = email;
            Pseudo = pseudo;
            AllowNewsletter = allowNewsletter;
            HashPwd = hashPwd;
            Role = MemberRoleEnum.Peon;
        }

        public Member(long id, string email, string? pseudo, bool allowNewsletter, MemberRoleEnum role, string? hashPwd = null)
            :this(email, pseudo, allowNewsletter, hashPwd)
        {
            Id = id;
            Role = role;
        }
    }
}
