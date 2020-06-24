using FluentAssertions;
using NUnit.Framework;
using TennisGame.Models;
using TennisGame.Persistent.NEventStore;
using TennisGame.Persistent.NEventStore.Configs;

namespace TennisGame.Tests.Persistents
{
    [TestFixture]
    public class NEventRepositoryTest
    {
        [TestCase(1)]
        [TestCase(100)]
        public void Append(int snapShotThreshold)
        {
            using var repo = new NEventRepository<Game, GameId>(new NEventConfig() { SnapShotThreshold = snapShotThreshold });
            var game = new Game(new GameId(1), new Player("1", "player1"), new Player("2", "player2"), 0, 0, GameStatus.Start);

            repo.PersistentEvent(game);
        }

        [TestCase(1)]
        [TestCase(100)]
        public void Get(int snapShotThreshold)
        {
            using var repo = new NEventRepository<Game, GameId>(new NEventConfig() { SnapShotThreshold = snapShotThreshold });
            var game = new Game(new GameId(1), new Player("1", "player1"), new Player("2", "player2"), 0, 0, GameStatus.Start);

            repo.PersistentEvent(game);
            var actual = repo.Rehydrate(game.Id);

            actual.Should().Be(game);
        }
    }
}