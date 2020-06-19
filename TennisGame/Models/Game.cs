using System;
using System.Collections.Generic;
using System.Linq;
using TennisGame.Commands;
using TennisGame.Policies;
using TennisGame.Services;

namespace TennisGame.Models
{
    public class Game
    {
        #region Constructors

        public Game()
        {
            Status = GameStatus.Start;
            Score = "Love - All";
        }

        public Game(Player player1, Player player2, int player1Score = 0, int player2Score = 0, GameStatus status = GameStatus.Start, int seq = 0)
            : this(new Team(Guid.NewGuid().ToString(), string.Empty, player1Score, player1),
                new Team(Guid.NewGuid().ToString(), string.Empty, player2Score, player2),
                status, seq)
        {
        }

        public Game(Team team1, Team team2, GameStatus status = GameStatus.Start, int seq = 0)
            : this()
        {
            if (new GameInitialPolicy().Validate(team1.Score, team2.Score, status) is (bool result, Exception ex) &&
                result == false)
                throw ex;

            Id = new GameId(seq);
            Teams = new[] { team1, team2 };
            Status = status;
        }

        #endregion Constructors

        #region Properties

        public GameId Id { get; }

        public GameStatus Status { get; private set; }

        public IReadOnlyCollection<Team> Teams { get; }

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
    }
}