using System;
using System.Collections.Generic;
using System.Linq;
using TennisGame.Commands;
using TennisGame.Core;
using TennisGame.Events;
using TennisGame.Policies;
using TennisGame.Services;

namespace TennisGame.Models
{
    public class Game : AggregateRoot<GameId>
    {
        #region Constructors

        public Game()
        {
        }

        public Game(GameId id, Player player1, Player player2, int player1Score = 0, int player2Score = 0,
            GameStatus status = GameStatus.Start, int appliedVersion = 1)
            : this(id, new Team(Guid.NewGuid().ToString(), string.Empty, player1Score, player1),
                new Team(Guid.NewGuid().ToString(), string.Empty, player2Score, player2),
                status, appliedVersion)
        {
        }

        public Game(GameId id, Team team1, Team team2, GameStatus status = GameStatus.Start, int appliedVersion = 1)
            : base(id, appliedVersion)
        {
            if (new GameInitialPolicy().Validate(team1.Score, team2.Score, status) is (bool result, Exception ex) &&
                result == false)
                throw ex;

            Teams = new[] { team1, team2 };
            Status = status;
            Score = "Love - All";

            RaiseEvent(new GameInitialEvent(EventVersion, Id, Teams, Status, Score));
        }

        #endregion Constructors

        #region Properties

        public GameStatus Status { get; private set; }

        public IReadOnlyCollection<Team> Teams { get; private set; }

        public string Score { get; private set; }

        #endregion Properties

        #region Public Methods

        public void LosePoint(LostPoint cmd)
        {
            var (team1Id, team2Id) = GetAllTeamId();
            if (new LosePointPolicy().Validate(cmd, team1Id, team2Id, Status) is (bool validateResult, Exception exception) &&
                validateResult == false)
                throw exception;

            var team = GetTeam(cmd.TeamId, cmd.PlayerId);
            team.Deduction();

            var (score, status) = new ScoreService().Judge(this);
            RaiseEvent(new LosePointEvent(EventVersion, cmd.TeamId, cmd.PlayerId, score, status));

            Score = score;
            Status = status;
        }

        public void WinPoint(WinPoint cmd)
        {
            var (team1Id, team2Id) = GetAllTeamId();
            if (new WinPointPolicy().Validate(cmd, team1Id, team2Id, Status) is (bool validateResult, Exception exception) &&
                validateResult == false)
                throw exception;

            var team = GetTeam(cmd.TeamId, cmd.PlayerId);
            team.AddPoint();

            var (score, status) = new ScoreService().Judge(this);
            RaiseEvent(new WinPointEvent(EventVersion, cmd.TeamId, cmd.PlayerId, score, status));

            Score = score;
            Status = status;
        }

        #endregion Public Methods

        #region Private Methods

        private (string, string) GetAllTeamId()
            => (Teams.First().Id, Teams.ElementAt(1).Id);

        private Team GetTeam(string teamId, string playerId)
            => Teams.First(t => t.Id.Equals(teamId) || t.ExistPlayer(playerId));

        #endregion Private Methods

        protected override void ApplyEvent(DomainEvent @event)
        {
            switch (@event)
            {
                case GameInitialEvent evt:
                    Id = evt.GameId;
                    EventVersion = evt.Version;
                    Teams = evt.Teams;
                    Status = evt.Status;
                    Score = evt.Score;
                    break;

                case WinPointEvent evt:
                    WinPoint(new WinPoint(evt.TeamId, evt.PlayerId));
                    break;

                case LosePointEvent evt:
                    LosePoint(new LostPoint(evt.TeamId, evt.PlayerId));
                    break;
            }
        }
    }
}