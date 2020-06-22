using System;
using System.Linq;
using FluentAssertions;
using TennisGame.Commands;
using TennisGame.Exceptions;
using TennisGame.Models;

namespace TennisGame.Tests.LosePoint
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

        protected void AssertException(string content = "")
        {
            var assert = _action.Should().Throw<AggregateException>()
                .WithInnerException<LostPointException>();

            if (string.IsNullOrWhiteSpace(content) == false)
                assert.WithMessage(content);
        }

        protected void SetGame(int player1Score = 0, int player2Score = 0, GameStatus status = GameStatus.Playing)
        {
            _game = new Game(new GameId(1), new Player(Guid.NewGuid().ToString(), "player1"),
                new Player(Guid.NewGuid().ToString(), "player2"), player1Score, player2Score, status);
        }

        protected void Lose(string teamId)
        {
            _game.LosePoint(new LostPoint(teamId));
        }

        protected void LoseForException(string teamId)
        {
            _action = () => Lose(teamId);
        }
    }
}