using System.Net;
using System.Runtime.CompilerServices;

namespace Enigma.Domain.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message, [CallerFilePath] string callerFile = "", 
            [CallerMemberName] string callerName = "", [CallerLineNumber] long callerLine = 0)
            : base(HttpStatusCode.Unauthorized, message, callerFile, callerName, callerLine)
        {
        }
    }
}
