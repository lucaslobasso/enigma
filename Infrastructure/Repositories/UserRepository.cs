using Enigma.Domain.Repositories;
using Enigma.Infrastructure.Contexts;

namespace Enigma.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<User> GetAsync(string username)
        {
            return await _context.Users.SingleAsync(w => w.Username == username);
        }

        public async Task<User?> TryGetAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(w => w.Username == username);
        }
        public async Task<bool> ExistsAsync(string username)
        {
            return await TryGetAsync(username) is not null;
        }
    }
}
