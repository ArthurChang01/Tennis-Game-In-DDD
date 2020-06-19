using MediatR;
using TennisGame.Applications.DTOs.Responses;

namespace TennisGame.Applications.DTOs.Requests
{
    public class WinPointRequest : IRequest<WinPointResponse>
    {
        public string GameId { get; set; }
        public string TeamId { get; set; }
        public string PlayerId { get; set; }
    }
}