using System;

namespace TennisGame.Commands
{
    public class LostPoint
    {
        public LostPoint(string teamId = "", string playerId = "")
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