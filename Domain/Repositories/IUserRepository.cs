using Enigma.Domain.Entities;

namespace Enigma.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> TryGetAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default);
    }
}
