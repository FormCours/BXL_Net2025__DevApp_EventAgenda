namespace Demo_WebAPI_EventAgenda.Domain.Models
{
    public class Faq
    {
        public long Id { get; private set; }
        public string Question { get; private set; }
        public string Response { get; private set; }
        public bool IsVisible { get; private set; }
        public int NbLike { get; private set; }


        public Faq() { }

        public Faq(string question, string response, bool isVisible = true)
        {
            if(string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(response))
            {
                throw new ArgumentNullException("Une FAQ doit contenir une question et une réponse !");
            }

            Question = question;
            Response = response;
            IsVisible = isVisible;
            NbLike = 0;
        }

        public Faq ChangeVisibility(bool visible)
        {
            IsVisible = visible;
            return this;
        }

        public Faq IncrLike()
        {
            NbLike++;
            return this;
        }
    }
}
