using LogLevel = Enigma.Domain.Enums.LogLevel;

namespace Enigma.API.DTOs.LogDTO
{
    public class CreateLogDTO
    {
        public Guid ApplicationId { get; set; }
        public LogLevel Level { get; set; } = LogLevel.Info;
        public string Message { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string CallerName { get; set; } = string.Empty;
        public long? LineNumber { get; set; }
    }
}
