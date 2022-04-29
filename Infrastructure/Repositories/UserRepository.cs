using Enigma.Domain.Repositories;
using Enigma.Infrastructure.Contexts;

namespace Enigma.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<User> GetAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleAsync(w => w.Email == email, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<User?> TryGetAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Email == email, cancellationToken)
                .ConfigureAwait(false);
        }
        public async Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            return await TryGetAsync(email, cancellationToken) is not null;
        }
    }
}
