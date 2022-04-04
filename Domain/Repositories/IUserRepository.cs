using Enigma.Domain.Entities;

namespace Enigma.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetAsync(string username);
        Task<User?> TryGetAsync(string username);
        Task<bool> ExistsAsync(string username);
    }
}
