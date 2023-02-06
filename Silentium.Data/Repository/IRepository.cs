using System.Linq.Expressions;

namespace Silentium.Data.Repository {
    public interface IRepository<T> where T : class {
        T Get(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Delete(int id);
    }
}
