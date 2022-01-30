using System.Linq.Expressions;
using DAL.Models;
using MongoDB.Driver;

namespace DAL.Database
{
    public class MongoEntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
    {

        private readonly IMongoCollection<TEntity> EntityCollection;
        private readonly IMongoCollection<ErrorLog> ErrorCollection;

        public MongoEntityRepository(string connectionString, string databaseName, string collectionName)
        {
            IMongoClient client = new MongoClient(connectionString);

            IMongoDatabase database = client.GetDatabase(databaseName);

            EntityCollection = database.GetCollection<TEntity>(collectionName);
            ErrorCollection = database.GetCollection<ErrorLog>("ErrorLog");
        }

        public TEntity Add(TEntity entity)
        {
            try
            {
                EntityCollection.InsertOne(entity);
                return entity;
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return null;

            }
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                EntityCollection.InsertMany(entities);
                return entities;
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return null;

            }
        }

        public bool Complete(FilterDefinition<TEntity> filter, TEntity entity)
        {
            try
            {
                EntityCollection.ReplaceOneAsync(filter, entity);
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return false;
            }
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return EntityCollection.Find(predicate).ToList();
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return null;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return EntityCollection.Find(x => true).ToList();
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return null;
            }
        }

        public bool Remove(FilterDefinition<TEntity> filter)
        {
            try
            {
                EntityCollection.DeleteOneAsync(filter);
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return false;
            }
        }

        public bool RemoveRange(FilterDefinition<TEntity> filter)
        {
            try
            {
                EntityCollection.DeleteMany(filter);
                return true;

            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return false;
            }
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return EntityCollection.Find(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return null;
            }
        }

        private void WriteLog(Exception ex)
        {
            ErrorLog log = new ErrorLog();
            log.CreatedDate = DateTime.Now;
            log.ExceptionMessage = ex.Message;
            log.ExceptionStackTrace = ex.StackTrace;
            ErrorCollection.InsertOne(log);
        }
    }
}