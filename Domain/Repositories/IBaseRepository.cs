using System.Linq.Expressions;

namespace Enigma.Domain.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default);
        Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<T?> TryGetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(T entity);
        void Update(T entity);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
