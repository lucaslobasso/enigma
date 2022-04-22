using Enigma.API.DTOs.ApplicationDTO;
using Enigma.Domain.Entities;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Repositories;

namespace Enigma.API.Services.ApplicationService
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _repository;

        public ApplicationService(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApplicationDTO> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity      = await _repository.GetAsync(id, cancellationToken);
            var application = new ApplicationDTO()
            {
                Id           = entity.Id,
                Denomination = entity.Denomination
            };

            return application;
        }

        public async Task<List<ApplicationDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities     = await _repository.GetAllAsync(null, cancellationToken);
            var applications = entities.Select(s => new ApplicationDTO() 
                { 
                    Id           = s.Id,
                    Denomination = s.Denomination
                })
            .ToList();

            return applications;
        }

        public async Task<Guid> Create(CreateApplicationDTO dto, CancellationToken cancellationToken = default)
        {
            var entity = new Application(
                dto.Denomination
            );

            _repository.Add(entity);
            await _repository.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task UpdateAync(UpdateApplicationDTO dto, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.TryGetAsync(dto.Id, cancellationToken);
            
            if (entity is null)
            {
                throw new ValidationException("Application does not exist.");
            }

            entity.UpdateDenomination(dto.Denomination);

            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var exists = await _repository.ExistsAsync(id, cancellationToken);

            if (!exists)
            {
                throw new ValidationException("Application does not exist.");
            }

            await _repository.DeleteAsync(id, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
