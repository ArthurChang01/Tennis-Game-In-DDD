using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TennisGame.Applications.DTOs.Requests;
using TennisGame.Applications.DTOs.Responses;
using TennisGame.Models;
using TennisGame.Persistent;

namespace TennisGame.Applications.UseCases
{
    public class WinPoint : IRequestHandler<WinPointRequest, WinPointResponse>
    {
        private readonly IMongoRepository<Game> _gameRepository;

        public WinPoint(IMongoRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<WinPointResponse> Handle(WinPointRequest request, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.GetOne(o => o.Id.Equals(request.GameId));

            game.WinPoint(new Commands.WinPoint(request.TeamId, request.PlayerId));

            await _gameRepository.Update(o => o.Id.Equals(game.Id), game);

            return new WinPointResponse();
        }
    }
}