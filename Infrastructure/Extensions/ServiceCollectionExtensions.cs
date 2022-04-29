using Enigma.Domain.Repositories;
using Enigma.Infrastructure.Bootstrap;
using Enigma.Infrastructure.Contexts;
using Enigma.Infrastructure.Repositories;
using Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Enigma.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.ConfigureOptions(configuration);
            services.ConfigureDatabase(configuration, environment);
            services.AddRepositories();
        }

        private static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlServerOptions = configuration.GetSection(SqlServerOptions.Section);
            services.Configure<SqlServerOptions>(sqlServerOptions.Bind);

            var authOptions = configuration.GetSection(AuthenticationOptions.Section);
            services.Configure<AuthenticationOptions>(authOptions.Bind);
        }

        private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            string? assemblyName = typeof(DatabaseContext).Assembly.GetName().Name;
            var sqlServerOptions = configuration
                .GetSection(SqlServerOptions.Section)
                .Get<SqlServerOptions>();

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(
                    sqlServerOptions.ConnectionString,
                    builder =>
                    {
                        builder.MigrationsAssembly(assemblyName);
                        builder.EnableRetryOnFailure(2);
                    });

                if (environment.IsDevelopment() || environment.IsStaging())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            services.AddHostedService<DatabaseBootstrap>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
        }
    }
}
