using System;

namespace TennisGame.Commands
{
    public class LostPoint
    {
        #region Constructors

        public LostPoint(string teamId = "", string playerId = "", int score = 0)
        {
            if (string.IsNullOrWhiteSpace(teamId) && string.IsNullOrWhiteSpace(playerId))
                throw new ArgumentException();

            TeamId = teamId;
            PlayerId = playerId;
            Score = score;
        }

        #endregion Constructors

        public string PlayerId { get; }
        public int Score { get; }
        public string TeamId { get; }
    }
}