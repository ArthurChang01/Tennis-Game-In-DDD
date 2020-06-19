using System;
using System.Linq;
using FluentAssertions;
using TennisGame.Commands;

namespace TennisGame.Tests.LosePoint
{
    public class TestClassBase
    {
        protected Game _game;

        protected string Team1Id => _game.Teams.First().Id;
        protected string Team2Id => _game.Teams.ElementAt(1).Id;

        protected void Assert(string actual)
        {
            _game.Score.Should().Be(actual);
        }

        protected void SetGame(int player1Score = 0, int player2Score = 0)
        {
            _game = new Game(new Player(Guid.NewGuid().ToString(), "player1"),
                new Player(Guid.NewGuid().ToString(), "player2"), player1Score, player2Score, GameStatus.Playing);
        }

        protected void Lose(string teamId)
        {
            _game.LosePoint(new LostPoint(teamId));
        }
    }
}