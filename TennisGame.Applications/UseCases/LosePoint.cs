using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TennisGame.Applications.DTOs.Requests;
using TennisGame.Applications.DTOs.Responses;
using TennisGame.Commands;
using TennisGame.Models;
using TennisGame.Repositories;

namespace TennisGame.Applications.UseCases
{
    public class LosePoint : IRequestHandler<LosePointRequest, LosePointResponse>
    {
        private readonly IGameRepository _gameRepository;

        public LosePoint(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<LosePointResponse> Handle(LosePointRequest request, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.Get(new GameId(request.GameId));

            game.LosePoint(new LostPoint(request.TeamId, request.PlayerId));

            await _gameRepository.UpdateScore(game);

            return new LosePointResponse();
        }
    }
}