using Enigma.API.DTOs.ApplicationDTO;

namespace Enigma.API.Services.ApplicationService
{
    public interface IApplicationService
    {
        Task<ApplicationDTO> GetAsync(Guid id, CancellationToken cancellationToken = default);

        Task<List<ApplicationDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Guid> Create(CreateApplicationDTO dto, CancellationToken cancellationToken = default);

        Task UpdateAync(UpdateApplicationDTO dto, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
