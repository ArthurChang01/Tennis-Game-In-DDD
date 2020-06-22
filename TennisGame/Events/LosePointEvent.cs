using TennisGame.Core;
using TennisGame.Models;

namespace TennisGame.Events
{
    public class LosePointEvent : DomainEvent
    {
        #region Constructors

        public LosePointEvent(int version, string teamId, string playerId, string newScore, GameStatus newStatus)
            : base(version)
        {
            TeamId = teamId;
            PlayerId = playerId;
            NewScore = newScore;
            NewStatus = newStatus;
        }

        #endregion Constructors

        #region Properties

        public string TeamId { get; }

        public string PlayerId { get; }

        public string NewScore { get; }

        public GameStatus NewStatus { get; }

        #endregion Properties
    }
}