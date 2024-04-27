using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserManagement.TableCreation;

namespace ProductsApiService.Infrastructure
{
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as User;
            if (user is null)
            {
                // context.Result = new ForbidResult(JwtBearerDefaults.AuthenticationScheme); //sends 401:Unauthorized/Forbidden response.
                context.Result = new JsonResult(
                    new { message = "You are not authorized to use this APIs. Contact your admin." })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }


    }
}
