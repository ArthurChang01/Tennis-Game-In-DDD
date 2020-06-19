using System;
using System.Linq;
using FluentAssertions;
using TennisGame.Exceptions;

namespace TennisGame.Tests.WinPoint
{
    public class TestClassBase
    {
        protected Game _game;
        private Action _action;

        protected string Team1Id => _game.Teams.First().Id;
        protected string Team2Id => _game.Teams.ElementAt(1).Id;

        protected void Assert(string actual)
        {
            _game.Score.Should().Be(actual);
        }

        protected void AssertException(string message = "")
        {
            var assert = _action.Should().Throw<AggregateException>()
                .WithInnerException<WinPointException>();

            if (string.IsNullOrWhiteSpace(message) == false)
                assert.WithMessage(message);
        }

        protected void BothWin(int score)
        {
            for (int i = 0; i < score; i++)
            {
                Win(Team1Id, 1);
                Win(Team2Id, 1);
            }
        }

        protected void SetGame(int player1Score = 0, int player2Score = 0, GameStatus status = GameStatus.Start)
        {
            _game = new Game(new Player(Guid.NewGuid().ToString(), "player1"),
                new Player(Guid.NewGuid().ToString(), "player2"),
                player1Score, player2Score, status);
        }

        protected void Win(string teamId, int score)
        {
            for (var i = 0; i < score; i++)
                _game.WinPoint(new Commands.WinPoint(teamId));
        }

        protected void WinForExcept(string teamId)
        {
            _action = () => Win(teamId, 1);
        }
    }
}