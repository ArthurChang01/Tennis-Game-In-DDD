using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using TennisGame.Applications.Serializers;
using TennisGame.Models;
using TennisGame.Persistent.EventStore;

namespace TennisGame.Tests.Persistents
{
    [TestFixture]
    public class ESRepositoryTest
    {
        private EventRepository<Game, GameId> _repo;

        public ESRepositoryTest()
        {
            _repo = new EventRepository<Game, GameId>(
               new ESConnection(new Uri("tcp://admin:changeit@localhost:1113"),
                   new Logger<ESConnection>(new NullLoggerFactory())),
               new GameSerializer());
        }

        [Test]
        public async Task Append()
        {
            var game = new Game(new GameId(1), new Player("1", "player1"), new Player("2", "player2"), 0, 0, GameStatus.Start);

            game.WinPoint(new Commands.WinPoint(game.Teams.First().Id));

            await _repo.Append(game);
        }

        [Test]
        public async Task Fetch()
        {
            Game game = null;
            Action act = () => game = _repo.Rehydrate(new GameId(1)).GetAwaiter().GetResult();

            act.Should().NotThrow();
        }
    }
}