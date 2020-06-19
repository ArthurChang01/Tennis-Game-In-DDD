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
    }
}