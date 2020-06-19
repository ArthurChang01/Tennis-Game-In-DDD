using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using TennisGame.Models;
using TennisGame.Repositories;

namespace TennisGame.Persistent
{
    public class GameRepository : MongoRepository, IGameRepository
    {
        public GameRepository(string conString) : base(conString)
        {
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            return await _collection.Find(o => true).ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetBy(Expression<Func<Game, bool>> @by)
        {
            return await _collection.Find(@by).ToListAsync();
        }

        public async Task<Game> Get(Expression<Func<Game, bool>> @by)
        {
            return await _collection.Find(@by).FirstOrDefaultAsync();
        }

        public Task Add(Game game)
        {
            return _collection.InsertOneAsync(game);
        }

        public Task Update(Game game)
        {
            return _collection.ReplaceOneAsync(o => o.Id.Equals(game.Id), game, new ReplaceOptions()
            {
                IsUpsert = true
            });
        }

        public Task Remove(Game game)
        {
            return _collection.DeleteOneAsync(o => o.Id.Equals(game.Id));
        }
    }
}