using System;
using System.Linq;
using FluentAssertions;
using TennisGame.Commands;

namespace TennisGame.Tests
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

        protected void BothWin(int score)
        {
            for (int i = 0; i < score; i++)
            {
                Win(Team1Id, 1);
                Win(Team2Id, 1);
            }
        }

        protected void SetGame()
        {
            _game = new Game(new Player(Guid.NewGuid().ToString(), "player1"),
                new Player(Guid.NewGuid().ToString(), "player2"));
        }

        protected void Win(string teamId, int score)
        {
            for (var i = 0; i < score; i++)
                _game.WinPoint(new WinPoint(teamId));
        }
    }
}