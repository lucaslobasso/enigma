using Enigma.API.Middleware;
using Enigma.API.Extensions;
using Enigma.Infrastructure.Extensions;
using NLog;
using NLog.Web;

var logger = LogManager.Setup()
                .LoadConfigurationFromAppSettings()
                .GetCurrentClassLogger();

try
{
    logger.Info("Starting Enigma v{Version}", typeof(Program).Assembly.GetName().Version);
    
    var builder = WebApplication.CreateBuilder(args);

    // NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddApplicationLayer(builder.Configuration);
    builder.Services.AddInfrastructureLayer(builder.Configuration);

    var app = builder.Build();

    // HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseMiddleware<ExceptionMiddleware>();

    app.Run();
}
catch (Exception e)
{
    logger.Error("Stopped Enigma because of exception: {NewLine}{Exception}", Environment.NewLine, e);
    throw;
}
finally
{
    LogManager.Shutdown();
}

namespace Enigma
{
    public partial class Program 
    { 

    } 
}