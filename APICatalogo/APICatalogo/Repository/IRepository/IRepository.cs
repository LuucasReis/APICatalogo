using System.Linq.Expressions;

namespace APICatalogo.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        public void Add(T entity);
        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        public Task<T> GetByIdAsync(int id);
        public Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        public void Remove(T obj);
        public void RemoveRange(IEnumerable<T> obj);
    }
}
