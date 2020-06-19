using MongoDB.Driver;
using TennisGame.Models;

namespace TennisGame.Persistent
{
    public class MongoRepository
    {
        private IMongoCollection<Game> _collection;

        public MongoRepository(string conString)
        {
            var client = new MongoClient(conString);
            var db = client.GetDatabase("Games");
            _collection = db.GetCollection<Game>("TennisGames");
        }
    }
}