using System.Collections.Generic;
using TennisGame.Core;
using TennisGame.Models;

namespace TennisGame.Events
{
    public class GameInitialEvent : DomainEvent
    {
        #region Constructors

        public GameInitialEvent(int version, GameId id, IReadOnlyCollection<Team> teams, GameStatus status, string score)
            : base(version)
        {
            Id = id;
            Teams = teams;
            Status = status;
            Score = score;
        }

        #endregion Constructors

        #region Properties

        public GameId Id { get; }

        public IReadOnlyCollection<Team> Teams { get; }

        public GameStatus Status { get; }

        public string Score { get; }

        #endregion Properties
    }
}