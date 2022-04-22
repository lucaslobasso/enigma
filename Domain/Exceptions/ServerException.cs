using System.Net;
using System.Runtime.CompilerServices;

namespace Enigma.Domain.Exceptions
{
    public class ServerException : BaseException
    {
        public ServerException(string message, [CallerFilePath] string callerFile = "", 
            [CallerMemberName] string callerName = "", [CallerLineNumber] long callerLine = 0)
            : base(HttpStatusCode.InternalServerError, message, callerFile, callerName, callerLine)
        {
        }
    }
}
