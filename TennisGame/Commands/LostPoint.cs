using System;

namespace TennisGame.Commands
{
    public class LostPoint
    {
        #region Constructors

        public LostPoint(string teamId = "", string playerId = "")
        {
            if (string.IsNullOrWhiteSpace(teamId) && string.IsNullOrWhiteSpace(playerId))
                throw new ArgumentException();

            TeamId = teamId;
            PlayerId = playerId;
        }

        #endregion Constructors

        #region Properties

        public string TeamId { get; }

        public string PlayerId { get; }

        #endregion Properties
    }
}