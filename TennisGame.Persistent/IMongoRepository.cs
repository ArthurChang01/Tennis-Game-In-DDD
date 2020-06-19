using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TennisGame.Persistent
{
    public interface IMongoRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> @by);

        Task<T> GetOne(Expression<Func<T, bool>> @by);

        Task Add(T entity);

        Task Update(Expression<Func<T, bool>> filter, T entity);

        Task Remove(Expression<Func<T, bool>> filter);
    }
}