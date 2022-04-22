using LogLevel = Enigma.Domain.Enums.LogLevel;

namespace Enigma.API.DTOs.LogDTO
{
    public class GetByLogDTO
    {
        public Guid? ApplicationId { get; set; }
        public LogLevel? Level { get; set; }
        public DateTime? DateTimeFrom { get; set; }
        public DateTime? DateTimeTo { get; set; }
    }
}
