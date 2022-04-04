using Enigma.API.Exceptions;
using System.Net;

namespace Enigma.API.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is BaseException e)
            {
                _logger.LogError("[{CallerFile}] {CallerName} ({CallerLine}): {Message} {NewLine}{StackTrace} {NewLIne}",
                    e.CallerFile, e.CallerName, e.CallerLine, e.Message, Environment.NewLine, e.StackTrace, Environment.NewLine);

                return ExceptionResponse(context, e.Message, e.StatusCode);
            }

            _logger.LogError("{Source}: {Message} {NewLine}{StackTrace} {NewLine}",
                exception.Source, exception.Message, Environment.NewLine, exception.StackTrace, Environment.NewLine);

            return ExceptionResponse(context, exception.Message);
        }

        private static Task ExceptionResponse(HttpContext context, string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode  = (int)statusCode;

            return context.Response.WriteAsync(
                new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content    = new StringContent(message)
                }
                .ToString());
        }
    }
}
