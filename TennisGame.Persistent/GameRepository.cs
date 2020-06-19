using TennisGame.Models;

namespace TennisGame.Persistent
{
    public class GameRepository : MongoRepository<Game>
    {
        public GameRepository(string conString, string dbName, string collectionName)
            : base(conString, dbName, collectionName)
        {
        }
    }
}