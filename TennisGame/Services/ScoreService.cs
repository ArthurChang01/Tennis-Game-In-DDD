using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisGame.Services
{
    public class ScoreService
    {
        private readonly IDictionary<int, string> _scoreMapping = new Dictionary<int, string>()
        {
            {0, "Love"}, {1, "Fifteen"}, {2, "Thirty"}, {3, "Forty"}
        };

        public (string score, GameStatus status) Judge(Game game)
        {
            var score = (game.Teams.First(), game.Teams.ElementAt(1)) switch
            {
                var (team1, team2) when team1.Score == team2.Score && team1.Score < 3 => $"{_scoreMapping[team1.Score]} - All",
                var (team1, team2) when team1.Score == team2.Score && team1.Score >= 3 => "Deuce",

                var (team1, team2) when team1.Score == 0 && team2.Score <= 3 => $"{_scoreMapping[0]} - {_scoreMapping[team2.Score]}",
                var (team1, team2) when team1.Score <= 3 && team2.Score == 0 => $"{_scoreMapping[team1.Score]} - {_scoreMapping[0]}",
                var (team1, team2) when team1.Score <= 3 && team2.Score <= 3 => $"{_scoreMapping[team1.Score]} - {_scoreMapping[team2.Score]}",

                var (team1, team2) when team1.Score >= 3 && team2.Score >= 3 && (team1.Score - team2.Score) == 1 => $"{team1.Id} Adv",
                var (team1, team2) when team1.Score >= 3 && team2.Score >= 3 && (team2.Score - team1.Score) == 1 => $"{team2.Id} Adv",
                var (team1, team2) when team1.Score >= 3 && team2.Score >= 3 && (team1.Score - team2.Score) == 2 => $"{team1.Id} Win",
                var (team1, team2) when team1.Score >= 3 && team2.Score >= 3 && (team2.Score - team1.Score) == 2 => $"{team2.Id} Win",

                var (team1, team2) when team1.Score >= 4 && team2.Score == 0 => $"{team1.Id} Win",
                var (team1, team2) when team1.Score == 0 && team2.Score >= 4 => $"{team2.Id} Win",

                _ => throw new ArgumentException($"Player1 Score: {game.Teams.First().Score}, Player2 Score: {game.Teams.ElementAt(1).Score}")
            };

            var status = game.Status switch
            {
                GameStatus.Start => GameStatus.Playing,
                GameStatus.Playing when score.Contains("Win") => GameStatus.End,
                _ => game.Status
            };

            return (score, status);
        }
    }
}