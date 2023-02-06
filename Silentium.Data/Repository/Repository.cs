using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Silentium.Data.Repository {
    public class Repository<T> : IRepository<T> where T : class {
        private readonly DbContext _context;

        public Repository(DbContext context) {
            _context = context;
        }

        public void Delete(int id) {
            var entity = _context.Set<T>().Find(id);
            if(entity is not null) _context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) {
            IEnumerable<T> entities = _context.Set<T>().Where(predicate);
            return entities;
        }

        public T Get(int id) {
            T entity = _context.Set<T>().Find(id);
            return entity;
        }

        public void Insert(T entity) {
            _context.Set<T>().Add(entity);
        }
    }
}
