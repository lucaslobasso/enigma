using Enigma.Domain.Interfaces;
using Enigma.Domain.Repositories;
using Enigma.Infrastructure.Contexts;
using System.Linq.Expressions;

namespace Enigma.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        protected readonly DatabaseContext _context;

        public BaseRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            var query = filter is null ? _context.Set<T>() : _context.Set<T>().Where(filter);

            return await query.AsNoTracking()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .SingleAsync(w => w.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<T?> TryGetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await TryGetAsync(id, cancellationToken) is not null;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetAsync(id, cancellationToken);
            _context.Set<T>().Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
