using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TennisGame.Applications.DTOs.Requests;
using TennisGame.Applications.DTOs.Responses;
using TennisGame.Commands;
using TennisGame.Models;
using TennisGame.Persistent;

namespace TennisGame.Applications.UseCases
{
    public class LosePoint : IRequestHandler<LosePointRequest, LosePointResponse>
    {
        private readonly IMongoRepository<Game> _gameRepository;

        public LosePoint(IMongoRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<LosePointResponse> Handle(LosePointRequest request, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.GetOne(o => o.Id.Equals(request.GameId));

            game.LosePoint(new LostPoint(request.TeamId, request.PlayerId));

            await _gameRepository.Update(o => o.Id.Equals(game.Id), game);

            return new LosePointResponse();
        }
    }
}