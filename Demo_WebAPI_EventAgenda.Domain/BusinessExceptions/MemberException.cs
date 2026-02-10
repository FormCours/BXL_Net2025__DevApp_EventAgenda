namespace Demo_WebAPI_EventAgenda.Domain.BusinessExceptions
{
    public class MemberException : Exception
    {
        public MemberException(string? message) : base(message) { }
    }

    public class MemberBadCredentialException : MemberException
    {
        public MemberBadCredentialException() 
            : base("Les informations d'identification sont invalide 눈_눈")
        { }
    }
}
