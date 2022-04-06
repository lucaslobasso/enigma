using Enigma.Domain.Entities;

namespace Enigma.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetAsync(string username, CancellationToken cancellationToken = default);
        Task<User?> TryGetAsync(string username, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string username, CancellationToken cancellationToken = default);
    }
}
