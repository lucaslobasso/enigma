using Enigma.Domain.Interfaces;
using Enigma.Domain.Repositories;
using Enigma.Infrastructure.Contexts;
using System.Linq.Expressions;

namespace Enigma.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        protected readonly DatabaseContext _context;

        public BaseRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            return filter is null ? await _context.Set<T>().ToListAsync() : await _context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _context.Set<T>().SingleAsync(w => w.Id == id);
        }

        public async Task<T?> TryGetAsync(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await TryGetAsync(id) is not null;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async void DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity);
        }

        public async void SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
