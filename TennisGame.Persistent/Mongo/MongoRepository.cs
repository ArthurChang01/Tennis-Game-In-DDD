using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace TennisGame.Persistent.Mongo
{
    public abstract class MongoRepository<T> : IMongoRepository<T>
    {
        protected readonly IMongoCollection<T> _collection;

        protected MongoRepository(string conString,
            string dbName, string collectionName)
        {
            var client = new MongoClient(conString);
            var db = client.GetDatabase(dbName);
            _collection = db.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAll()
            => await _collection.Find(o => true).ToListAsync();

        public async Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> @by)
        => await _collection.Find(@by).ToListAsync();

        public async Task<T> GetOne(Expression<Func<T, bool>> @by)
        => await _collection.Find(@by).FirstOrDefaultAsync();

        public Task Add(T entity)
            => _collection.InsertOneAsync(entity);

        public Task Update(Expression<Func<T, bool>> filter, T entity)
            => _collection.ReplaceOneAsync(filter, entity, new ReplaceOptions() { IsUpsert = true });

        public Task Remove(Expression<Func<T, bool>> filter)
            => _collection.DeleteOneAsync(filter);
    }
}