using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TennisGame.Models;

namespace TennisGame.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAll();

        Task<IEnumerable<Game>> GetBy(Expression<Func<bool, Game>> @by);

        Task<Game> Get(Expression<Func<bool, Game>> @by);

        Task Add(Game game);

        Task Update(Game game);

        Task Remove(Game game);
    }
}