using Enigma.Domain.Enums;

namespace Enigma.Domain.Entities
{
    public class Log : BaseEntity
    {
        public Log(Guid applicationId, LogLevel level, string message, string fileName = "", string callerName = "", long? lineNumber = null)
        {
            ApplicationId   = applicationId;
            Level           = level;
            Message         = message;
            FileName        = fileName;
            CallerName      = callerName;
            LineNumber      = lineNumber;
            CreatedDateTime = DateTime.Now;
        }

        public Guid ApplicationId { get; internal set; }
        public LogLevel Level { get; internal set; }
        public string Message { get; internal set; }
        public string FileName { get; internal set; }
        public string CallerName { get; internal set; }
        public long? LineNumber { get; internal set; }
        public DateTime CreatedDateTime { get; internal set; }
    }
}
