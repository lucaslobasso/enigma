using Enigma.API.DTOs.LogDTO;
using Enigma.Domain.Entities;
using Enigma.Domain.Repositories;

namespace Enigma.API.Services.LogService
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _repository;

        public LogService(ILogRepository repository)
        {
            _repository = repository;
        }

        public async Task<LogDTO> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(id, cancellationToken);
            var log    = new LogDTO()
            {
                ApplicationId   = entity.ApplicationId,
                Level           = entity.Level,
                Message         = entity.Message,
                FileName        = entity.FileName,
                CallerName      = entity.CallerName,
                LineNumber      = entity.LineNumber,
                CreatedDateTime = entity.CreatedDateTime
            };

            return log;
        }

        public async Task<List<LogDTO>> GetByAsync(GetByLogDTO getBy, CancellationToken cancellationToken = default)
        {
            var entities = await _repository.GetAllAsync(w => 
                        (getBy.ApplicationId == null || w.ApplicationId.Equals(getBy.ApplicationId)) && 
                        (getBy.Level == null || w.Level.Equals(getBy.Level)) &&
                        (getBy.DateTimeFrom == null || w.CreatedDateTime >= getBy.DateTimeFrom) && 
                        (getBy.DateTimeTo == null || w.CreatedDateTime <= getBy.DateTimeTo), 
                    cancellationToken);

            var logs = entities.Select(s => new LogDTO() 
                { 
                    ApplicationId   = s.ApplicationId,
                    Level           = s.Level,
                    Message         = s.Message,
                    FileName        = s.FileName,
                    CallerName      = s.CallerName,
                    LineNumber      = s.LineNumber,
                    CreatedDateTime = s.CreatedDateTime
                })
            .ToList();

            return logs;
        }

        public async Task Create(CreateLogDTO dto, CancellationToken cancellationToken = default)
        {
            var entity = new Log(
                dto.ApplicationId,
                dto.Level,
                dto.Message,
                dto.FileName,
                dto.CallerName,
                dto.LineNumber
            );

            _repository.Add(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
