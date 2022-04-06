using Enigma.API.Middleware;
using Enigma.API.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace Enigma.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddTransient<ExceptionMiddleware>();
            services.AddSwagger();
            services.AddAuthentication(configuration);
            services.AddServices();
        }

        private static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Enigma API", Version = "v1" });
                options.SupportNonNullableReferenceTypes();
                options.AddSecurityDefinition("OAuth 2.0", new OpenApiSecurityScheme
                {
                    Name        = "Authorization",
                    Description = "Standard authorization header using the Bearer scheme ('bearer [token]')",
                    Type        = SecuritySchemeType.ApiKey,
                    In          = ParameterLocation.Header
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer           = false,
                        ValidateAudience         = false,
                        IssuerSigningKey         = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration.GetValue<string>("Authentication:Token"))
                        )
                    };
                });
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
