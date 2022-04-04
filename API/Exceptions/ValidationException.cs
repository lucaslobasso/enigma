using System.Net;
using System.Runtime.CompilerServices;

namespace Enigma.API.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message, [CallerFilePath] string callerFile = "", 
            [CallerMemberName] string callerName = "", [CallerLineNumber] long callerLine = 0) 
            : base(HttpStatusCode.BadRequest, message, callerFile, callerName, callerLine)
        {
        }
    }
}
