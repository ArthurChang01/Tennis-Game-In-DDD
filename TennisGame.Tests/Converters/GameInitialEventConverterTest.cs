using System;
using System.Text.Json;
using FluentAssertions;
using NUnit.Framework;
using TennisGame.Applications.Serializers.JsonConverters;
using TennisGame.Events;
using TennisGame.Models;

namespace TennisGame.Tests.Converters
{
    [TestFixture]
    public class GameInitialEventConverterTest
    {
        [Test]
        public void Serialize()
        {
            var ge = new GameInitialEvent("1", 1, DateTimeOffset.Now, new GameId(), new[] { new Team("1", "team", 0, new[] { new Player("1", "player", "1"), }), }, GameStatus.Start, "score");
            var opt = new JsonSerializerOptions();
            opt.Converters.Add(new GameIdConverter());
            opt.Converters.Add(new PlayersConverter());
            opt.Converters.Add(new TeamConverter());
            opt.Converters.Add(new GameInitialEventConverter());

            var actual = JsonSerializer.Serialize(ge, opt);

            actual.Should().Be($"{{\"Id\":\"1\",\"Version\":1,\"OccuredDate\":\"{ge.OccuredDate:yyyy/MM/dd HH:mm:ss}\",\"GameId\":{{\"GameId\":\"{ge.GameId}\"}},\"Teams\":[{{\"Id\":\"1\",\"Name\":\"team\",\"Players\":[{{\"Id\":\"1\",\"Name\":\"player\",\"TeamId\":\"1\"}}],\"Score\":0}}],\"Status\":\"Start\",\"Score\":\"score\"}}");
        }

        [Test]
        public void Deserialize()
        {
            var ge = new GameInitialEvent("1", 1, DateTimeOffset.Now, new GameId(), new[] { new Team("1", "team", 0, new[] { new Player("1", "player", "1"), }), }, GameStatus.Start, "score");
            var opt = new JsonSerializerOptions();
            opt.Converters.Add(new GameIdConverter());
            opt.Converters.Add(new PlayersConverter());
            opt.Converters.Add(new TeamConverter());
            opt.Converters.Add(new GameInitialEventConverter());

            var str = JsonSerializer.Serialize(ge, opt);
            var actual = JsonSerializer.Deserialize<GameInitialEvent>(str, opt);

            actual.Should().Be(ge);
        }
    }
}