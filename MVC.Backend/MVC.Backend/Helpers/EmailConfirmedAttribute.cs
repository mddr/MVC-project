using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace MVC.Backend.Helpers
{
    /// <summary>
    /// Atrybut sprawdzający czy użytkownik ma potwierdzony mail
    /// </summary>
    public class EmailConfirmedAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Sprawdza czy mail jest potwierdzony
        /// </summary>
        /// <param name="context">Kontekst uwieżytelnienia</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
                return;

            if (user.Claims.Where(c => c.Type == "Confirmed").Select(c => c.Value).SingleOrDefault() != "True")
                context.Result = new ForbidResult();
        }
    }
}
