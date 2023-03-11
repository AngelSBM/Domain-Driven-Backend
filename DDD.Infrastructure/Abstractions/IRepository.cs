using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infrastructure.Abstractions
{
    public interface IRepository<T> where T : class
    {
        public Task Add(T entity);
        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Find(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> expression);
        public Task<bool> Exists(Expression<Func<T, bool>> expression);
        public Task Remove(int id);
    }
}
