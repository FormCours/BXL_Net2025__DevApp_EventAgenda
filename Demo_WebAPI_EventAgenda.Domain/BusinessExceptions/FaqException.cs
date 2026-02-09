using Demo_WebAPI_EventAgenda.Domain.Models;


namespace Demo_WebAPI_EventAgenda.Domain.BusinessExceptions
{
    public class FaqException : Exception
    {
        public FaqException(string message) : base(message) { }
    }


    public class FaqNotFoundException : FaqException
    {
        public FaqNotFoundException() : base("FAQ non trouvé !") { }
    }

    public class FaqUpdateException : FaqException
    {
        public FaqUpdateException(string message) : base(message) { }
    }
}
