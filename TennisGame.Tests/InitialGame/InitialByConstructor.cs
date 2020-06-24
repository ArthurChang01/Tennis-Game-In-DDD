using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TennisGame.Core;
using TennisGame.Events;
using TennisGame.Models;

namespace TennisGame.Tests.InitialGame
{
    [TestFixture]
    public class InitialByConstructor
    {
        [Test]
        public void InitialByDefaultConstructor()
        {
            Action act = () => new Game();

            act.Should().NotThrow();
        }

        [Test]
        public void InitialByConstructor_With_Player()
        {
            Action act = () => new Game(new GameId(1), new Player(Guid.NewGuid().ToString(), "player1"),
                new Player(Guid.NewGuid().ToString(), "player2"), 0, 0);

            act.Should().NotThrow();
        }

        [Test]
        public void InitialByConstructor_With_Team()
        {
            Action act = () => new Game(new GameId(1), new Team(), new Team());

            act.Should().NotThrow();
        }

        [Test]
        public void InitialByEvent()
        {
            var teams = new List<Team>() { new Team(), new Team() };
            var events = new List<DomainEvent>()
            {
                new GameInitialEvent( new GameId(1), teams, GameStatus.Playing, "Love - All"),
                new WinPointEvent( teams.First().Id, string.Empty, "Fifteen - Love", GameStatus.Playing),
                new LosePointEvent( teams.First().Id, string.Empty, "Love - All", GameStatus.Playing ),
                new WinPointEvent( teams.First().Id, string.Empty, "Fifteen - Love", GameStatus.Playing)
            };
            Game game = null;

            Action act = () =>
            {
                game = (Game)AggregateRoot<GameId>.Create(new Game(), events);
            };

            act.Should().NotThrow();
            game.Score.Should().Be("Fifteen - Love");
            game.Events.Count.Should().Be(0);
        }
    }
}