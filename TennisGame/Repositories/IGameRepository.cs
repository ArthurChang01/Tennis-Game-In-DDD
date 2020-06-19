using System.Threading.Tasks;
using TennisGame.Models;

namespace TennisGame.Repositories
{
    public interface IGameRepository
    {
        Task<Game> Get(GameId id);

        Task UpdateScore(Game game);
    }
}