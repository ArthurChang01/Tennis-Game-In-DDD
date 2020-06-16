using System;
using System.Collections.Generic;
using System.Linq;
using TennisGame.Commands;
using TennisGame.Policies;
using TennisGame.Services;

namespace TennisGame
{
    public class Game
    {
        #region Fields

        private GameStatus _status;

        #endregion Fields

        #region Constructors

        public Game()
        {
            Teams = new[] { new Team(), new Team() };
            _status = GameStatus.Start;
        }

        public Game(Player player1, Player player2, int player1Score = 0, int player2Score = 0, GameStatus status = GameStatus.Start)
        {
            Teams = new[] {
                new Team(Guid.NewGuid().ToString(), string.Empty, player1Score,  player1),
                new Team(Guid.NewGuid().ToString(), string.Empty, player2Score,  player2)
            };

            if (new GameInitialPolicy().Validate(player1Score, player2Score, status) is (bool result, Exception ex) &&
                result == false)
                throw ex;


            _status = status;
        }

        public Game(Team team1, Team team2, GameStatus status = GameStatus.Start)
        {
            if (new GameInitialPolicy().Validate(team1.Score, team2.Score, status) is (bool result, Exception ex) &&
                result == false)
                throw ex;
                   
            Teams = new[] { team1, team2 };
            _status = status;
        }

        public GameStatus Status => _status;
        public IReadOnlyCollection<Team> Teams { get; }

        #endregion Constructors

        public string Score { get; private set; } = "Love - All";

        #region Public Methods

        public void LosePoint(LostPoint cmd)
        {
            var (team1Id, team2Id) = (Teams.First().Id, Teams.ElementAt(1).Id);
            if (new LosePointPolicy().Validate(cmd, team1Id, team2Id, cmd.Score, _status) is (bool validateResult, Exception exception)  &&
                validateResult == false)
                throw exception;

            var team = this.GetTeam(cmd.TeamId, cmd.PlayerId);
            team.DeductScore(cmd.Score);

            var result = new ScoreService().Judge(this);
            this.Score = result.score;
            _status = result.status;
        }

        public void WinPoint(WinPoint cmd)
        {
            var (team1Id, team2Id) = (Teams.First().Id, Teams.ElementAt(1).Id);
            if (new WinPointPolicy().Validate(cmd, team1Id, team2Id, cmd.Score, _status) == false)
                throw new ArgumentException($"{team1Id}, {team2Id}, {cmd.TeamId}, {cmd.Score}");

            var team = this.GetTeam(cmd.TeamId, cmd.PlayerId);
            team.AddScore(cmd.Score);

            var result = new ScoreService().Judge(this);
            this.Score = result.score;
            _status = result.status;
        }

        #endregion Public Methods

        #region Private Methods

        private Team GetTeam(string teamId, string playerId)
            => Teams.First(t => t.Id.Equals(teamId) || t.ExistPlayer(playerId));

        #endregion Private Methods
    }
}