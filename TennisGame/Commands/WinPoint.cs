using System;

namespace TennisGame.Commands
{
    public class WinPoint
    {
        public WinPoint(string teamId = "", string playerId = "")
        {
            if (string.IsNullOrWhiteSpace(teamId) && string.IsNullOrWhiteSpace(playerId))
                throw new ArgumentException();

            TeamId = teamId;
            PlayerId = playerId;
        }

        public string TeamId { get; }

        public string PlayerId { get; }
    }
}