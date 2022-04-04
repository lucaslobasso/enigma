using System.Net;
using System.Runtime.CompilerServices;

namespace Enigma.API.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(HttpStatusCode statusCode, string message, [CallerFilePath] string callerFile = "",
                [CallerMemberName] string callerName = "", [CallerLineNumber] long callerLine = 0) : base(message)
        {
            StatusCode = statusCode;
            CallerFile = callerFile;
            CallerName = callerName;
            CallerLine = callerLine;
        }

        public HttpStatusCode StatusCode { get; internal set; }
        public string CallerFile { get; internal set; }
        public string CallerName { get; internal set; }
        public long CallerLine { get; internal set; }
    }
}
