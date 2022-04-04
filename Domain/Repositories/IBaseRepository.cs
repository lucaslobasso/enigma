using System.Linq.Expressions;

namespace Enigma.Domain.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Guid id);
        Task<T?> TryGetAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        void Add(T entity);
        void Update(T entity);
        void DeleteAsync(Guid id);
        void SaveChangesAsync();
    }
}
