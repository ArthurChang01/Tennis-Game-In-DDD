using System;
using TennisGame.Core;
using TennisGame.Models;

namespace TennisGame.Events
{
    public class WinPointEvent : DomainEvent
    {
        #region Constructors

        public WinPointEvent()
        {
        }

        public WinPointEvent(string id, int version, DateTimeOffset occuredDate, string teamId, string playerId, string newScore, GameStatus newStatus)
            : base(id, version, occuredDate)
        {
            TeamId = teamId;
            PlayerId = playerId;
            NewScore = newScore;
            NewStatus = newStatus;
        }

        public WinPointEvent(int version, string teamId, string playerId, string newScore, GameStatus newStatus)
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

        public override bool Equals(object? obj)
        {
            if (!(obj is WinPointEvent target))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return Equals(this, target) && TeamId.Equals(target.TeamId) && PlayerId.Equals(target.PlayerId) &&
                   NewScore.Equals(target.NewScore) && NewStatus.Equals(target.NewStatus);
        }

        public override int GetHashCode()
            => (GetHashCode(this), TeamId, PlayerId, NewScore, NewStatus).GetHashCode();
    }
}