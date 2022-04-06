using Enigma.Domain.Repositories;
using Enigma.Infrastructure.Contexts;
using Enigma.Infrastructure.Repositories;
using Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions(configuration);
            services.ConfigureDatabase(configuration);
            services.AddRepositories();
        }

        private static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlServerOptions = configuration.GetSection(SqlServerOptions.Section);
            services.Configure<SqlServerOptions>(sqlServerOptions.Bind);

            var authOptions = configuration.GetSection(AuthenticationOptions.Section);
            services.Configure<AuthenticationOptions>(authOptions.Bind);
        }

        private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlServerOptions = configuration
                .GetSection(SqlServerOptions.Section)
                .Get<SqlServerOptions>();

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(sqlServerOptions.ConnectionString)
            );
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
