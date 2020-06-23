using System.Text.Json;
using FluentAssertions;
using NUnit.Framework;
using TennisGame.Models;
using TennisGame.Persistent.EventStore.JsonConverters;

namespace TennisGame.Tests.Converters
{
    [TestFixture]
    public class GameIdConverterTest
    {
        [Test]
        public void Serialize()
        {
            var gameId = new GameId(1);
            var opt = new JsonSerializerOptions();
            opt.Converters.Add(new GameIdConverter());

            var actual = JsonSerializer.Serialize(gameId, opt);

            actual.Should().Be($"{{\"GameId\":\"{gameId}\"}}");
        }

        [Test]
        public void Deserialize()
        {
            var gameId = new GameId(1);
            var opt = new JsonSerializerOptions();
            opt.Converters.Add(new GameIdConverter());

            var str = JsonSerializer.Serialize(gameId, opt);
            var actual = JsonSerializer.Deserialize<GameId>(str);

            actual.Should().Be(gameId);
        }
    }
}