using System.Linq.Expressions;
using MongoDB.Driver;

namespace DAL.Database
{
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        bool Remove(FilterDefinition<TEntity> filter);
        bool RemoveRange(FilterDefinition<TEntity> filter);
        bool Complete(FilterDefinition<TEntity> filter, TEntity entity);
    }
}