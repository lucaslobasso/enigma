using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Enigma.Infrastructure.Contexts;

namespace Enigma.Infrastructure.Bootstrap
{
    public class DatabaseBootstrap : IHostedService
    {
        private readonly ILogger<DatabaseBootstrap> _logger;
        private readonly IServiceProvider _serviceProvider;

        public DatabaseBootstrap(ILogger<DatabaseBootstrap> logger, IServiceProvider serviceProvider)
        {
            _logger          = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("[DatabaseBootstrap] Applying database migrations.");

                    using (IServiceScope scope     = _serviceProvider.CreateScope())
                    using (DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>())
                    {
                        await context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
                        _logger.LogInformation("[DatabaseBootstrap] Database migrations applied successfully.");
                        break;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "[DatabaseBootstrap] Error while applying database migrations.");
                }

                await Task.Delay(3000, cancellationToken).ConfigureAwait(false);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}