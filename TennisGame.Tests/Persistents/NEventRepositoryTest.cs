using FluentAssertions;
using NUnit.Framework;
using TennisGame.Models;
using TennisGame.Persistent.NEventStore;

namespace TennisGame.Tests.Persistents
{
    [TestFixture]
    public class NEventRepositoryTest
    {
        [Test]
        public void Append()
        {
            using var repo = new NEventRepository<Game, GameId>();
            var game = new Game(new GameId(1), new Player("1", "player1"), new Player("2", "player2"), 0, 0, GameStatus.Start);

            repo.PersistentEvent(game);
        }

        [Test]
        public void Get()
        {
            using var repo = new NEventRepository<Game, GameId>();
            var game = new Game(new GameId(1), new Player("1", "player1"), new Player("2", "player2"), 0, 0, GameStatus.Start);

            repo.PersistentEvent(game);
            var actual = repo.Rehydrate(game.Id);

            actual.Should().Be(game);
        }
    }
}