using System.Threading.Tasks;
using MongoDB.Driver;
using TennisGame.Models;
using TennisGame.Repositories;

namespace TennisGame.Persistent
{
    public class GameRepository : MongoRepository<Game>, IGameRepository
    {
        public GameRepository(string conString, string dbName, string collectionName)
            : base(conString, dbName, collectionName)
        {
        }

        public Task<Game> Get(GameId id)
            => base.GetOne(o => o.Id.Equals(id));

        public Task UpdateScore(Game game)
        {
            var filter = Builders<Game>.Filter.Eq(o => o.Id, game.Id);
            var updater = Builders<Game>.Update.Set(o => o.Score, game.Score);

            return _collection.UpdateOneAsync(filter, updater);
        }
    }
}