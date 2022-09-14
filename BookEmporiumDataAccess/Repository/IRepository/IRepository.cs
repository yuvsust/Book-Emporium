
using System.Linq.Expressions;

namespace BookEmporium.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetFirstOrDefault(Expression<Func<T,bool>> filter, string? includeProperties = null);
        Task<IEnumerable<T>> GetAll(string? includeProperties = null);
        Task Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
