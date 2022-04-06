using Enigma.Domain.Repositories;
using Enigma.Infrastructure.Contexts;

namespace Enigma.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<User> GetAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleAsync(w => w.Username == username, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<User?> TryGetAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Username == username, cancellationToken)
                .ConfigureAwait(false);
        }
        public async Task<bool> ExistsAsync(string username, CancellationToken cancellationToken = default)
        {
            return await TryGetAsync(username, cancellationToken) is not null;
        }
    }
}
