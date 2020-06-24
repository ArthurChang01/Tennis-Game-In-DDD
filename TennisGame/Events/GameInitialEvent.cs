using System;
using System.Collections.Generic;
using TennisGame.Core;
using TennisGame.Models;

namespace TennisGame.Events
{
    public class GameInitialEvent : DomainEvent
    {
        #region Constructors

        public GameInitialEvent()
        {
        }

        public GameInitialEvent(GameId gameId, IReadOnlyCollection<Team> teams, GameStatus status, string score)
            : this(Guid.NewGuid().ToString(), 1, DateTimeOffset.Now, gameId, teams, status, score)
        {
        }

        public GameInitialEvent(string id, int version, DateTimeOffset occuredDate, GameId gameId, IReadOnlyCollection<Team> teams, GameStatus status, string score)
            : base(id, version, occuredDate)
        {
            GameId = gameId;
            Teams = teams;
            Status = status;
            Score = score;
        }

        #endregion Constructors

        #region Properties

        public GameId GameId { get; }

        public IReadOnlyCollection<Team> Teams { get; }

        public GameStatus Status { get; }

        public string Score { get; }

        #endregion Properties

        public override bool Equals(object? obj)
        {
            if (!(obj is GameInitialEvent target))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return Equals(this, target) && GameId.Equals(target.GameId) && Status.Equals(target.Status) &&
                   Score.Equals(target.Score);
        }

        public override int GetHashCode()
        {
            return (GetHashCode(this), GameId, Teams, Status, Score).GetHashCode();
        }
    }
}