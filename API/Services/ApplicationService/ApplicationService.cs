using Enigma.API.DTOs.ApplicationDTO;
using Enigma.Domain.Entities;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Repositories;
using System.Security.Claims;

namespace Enigma.API.Services.ApplicationService
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _repository;
        private readonly IHttpContextAccessor   _httpContextAccessor;

        public ApplicationService(IApplicationRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository          = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationDTO> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(id, cancellationToken);

            if (entity.UserId != GetCurrentUserId())
            {
                throw new ValidationException("Application does not exist.");
            }

            var application = new ApplicationDTO()
            {
                Id           = entity.Id,
                Denomination = entity.Denomination
            };

            return application;
        }

        public async Task<List<ApplicationDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities     = await _repository.GetAllAsync(w => w.UserId == GetCurrentUserId(), cancellationToken);
            var applications = entities.Select(s => new ApplicationDTO() 
                { 
                    Id           = s.Id,
                    Denomination = s.Denomination
                })
            .ToList();

            return applications;
        }

        public async Task<Guid> CreateAsync(CreateApplicationDTO dto, CancellationToken cancellationToken = default)
        {
            var entity = new Application(GetCurrentUserId(), dto.Denomination);

            _repository.Add(entity);
            await _repository.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task UpdateAync(UpdateApplicationDTO dto, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.TryGetAsync(dto.Id, cancellationToken);
            
            if (entity is null || entity.UserId != GetCurrentUserId())
            {
                throw new ValidationException("Application does not exist.");
            }

            entity.UpdateDenomination(dto.Denomination);
            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.TryGetAsync(id, cancellationToken);

            if (entity is null || entity.UserId != GetCurrentUserId())
            {
                throw new ValidationException("Application does not exist.");
            }

            await _repository.DeleteAsync(id, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        private Guid GetCurrentUserId()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var subClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!Guid.TryParse(subClaim, out var userId))
                {
                    throw new UnauthorizedException("Invalid logged user Id.");
                }

                return userId;
            }

            throw new UnauthorizedException("Must be logged in.");
        }
    }
}
