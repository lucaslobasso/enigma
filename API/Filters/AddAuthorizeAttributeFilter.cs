using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Enigma.API.Filters
{
    public class AddAuthorizeAttributeFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var isAuthorized = // This excludes controllers with AllowAnonymous attribute and includes the ones with Authorize attribute.
                               (context.MethodInfo.DeclaringType?.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ?? false &&
                               !context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any()) ||
                               // This excludes methods with AllowAnonymous attribute and includes the ones with Authorize attribute.
                               (context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() &&
                               !context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any());

            if (!isAuthorized)
            {
                return;
            }

            var jwtbearerScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    [jwtbearerScheme] = new string [] {}
                }
            };
        }
    }
}
