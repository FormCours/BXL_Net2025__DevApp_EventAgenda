using Demo_WebAPI_EventAgenda.Domain.BusinessExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.ExceptionHandlers
{
    public class AgendaEventExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // Test de garde -> On traite uniquement les exceptions de types "AgendaEventException"
            if (exception is not AgendaEventException)
                return false;

            int statusCode = (exception is AgendaEventNotFoundException) ? StatusCodes.Status404NotFound
                : (exception is AgendaEventCreateException) ? StatusCodes.Status422UnprocessableEntity
                : StatusCodes.Status400BadRequest;


            // Création de l'erreur à envoyer
            ProblemDetails problem = new ProblemDetails()
            {
                  Title = "AgendaEvent error !",
                  Detail = exception.Message,
                  Status = statusCode
            };

            // Cloture de la requete 
            // - Définition du status de la réponse
            httpContext.Response.StatusCode = statusCode;
            // - Envoyer la réponse
            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

            // Booléen « vrai » pour indiquer que l'exception a été traiter
            return true;
        }
    }
}
