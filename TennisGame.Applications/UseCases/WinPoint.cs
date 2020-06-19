using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TennisGame.Applications.DTOs.Requests;
using TennisGame.Applications.DTOs.Responses;
using TennisGame.Models;
using TennisGame.Repositories;

namespace TennisGame.Applications.UseCases
{
    public class WinPoint : IRequestHandler<WinPointRequest, WinPointResponse>
    {
        private readonly IGameRepository _gameRepository;

        public WinPoint(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<WinPointResponse> Handle(WinPointRequest request, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.Get(new GameId(request.GameId));

            game.WinPoint(new Commands.WinPoint(request.TeamId, request.PlayerId));

            await _gameRepository.UpdateScore(game);

            return new WinPointResponse();
        }
    }
}