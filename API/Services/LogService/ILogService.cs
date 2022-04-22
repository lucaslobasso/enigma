using Enigma.API.DTOs.LogDTO;

namespace Enigma.API.Services.LogService
{
    public interface ILogService
    {
        Task<LogDTO> GetAsync(Guid id, CancellationToken cancellationToken = default);

        Task<List<LogDTO>> GetByAsync(GetByLogDTO getBy, CancellationToken cancellationToken = default);

        Task Create(CreateLogDTO dto, CancellationToken cancellationToken = default);
    }
}
