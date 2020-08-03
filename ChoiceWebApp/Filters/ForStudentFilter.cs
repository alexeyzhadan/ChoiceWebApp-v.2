using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChoiceWebApp.Filters
{
    public class ForStudentFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.HasClaim(c => c.Type == "FullName"))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}