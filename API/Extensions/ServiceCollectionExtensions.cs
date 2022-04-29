using Enigma.API.Filters;
using Enigma.API.Middleware;
using Enigma.API.Services.ApplicationService;
using Enigma.API.Services.LogService;
using Enigma.API.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
                options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type         = SecuritySchemeType.Http,
                    Scheme       = "bearer",
                    BearerFormat = "JWT",
                    Description  = "JWT Authorization header using the Bearer scheme."
                });
                options.OperationFilter<AddAuthorizeAttributeFilter>();
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
                        ValidateLifetime         = true,
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
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<ILogService, LogService>();
            services.AddHttpContextAccessor();
        }
    }
}
