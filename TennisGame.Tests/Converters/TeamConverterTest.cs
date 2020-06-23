using System.Text.Json;
using FluentAssertions;
using NUnit.Framework;
using TennisGame.Models;
using TennisGame.Persistent.EventStore.JsonConverters;

namespace TennisGame.Tests.Converters
{
    [TestFixture]
    public class TeamConverterTest
    {
        [Test]
        public void Serialize()
        {
            var team = new Team("1", "team", 1, new[] { new Player("1", "player", "1"), });
            var opt = new JsonSerializerOptions();
            opt.Converters.Add(new PlayersConverter());
            opt.Converters.Add(new TeamConverter());

            var actual = JsonSerializer.Serialize(team, opt);

            actual.Should().Be($"{{\"Id\":\"{team.Id}\",\"Name\":\"{team.Name}\",\"Players\":[{{\"Id\":\"1\",\"Name\":\"player\",\"TeamId\":\"1\"}}],\"Score\":{team.Score}}}");
        }

        [Test]
        public void Deserialize()
        {
            var team = new Team("1", "team", 1, new[] { new Player("1", "player", "1"), });
            var opt = new JsonSerializerOptions();
            opt.Converters.Add(new PlayersConverter());
            opt.Converters.Add(new TeamConverter());

            var str = JsonSerializer.Serialize(team, opt);
            var actual = JsonSerializer.Deserialize<Team>(str, opt);
        }
    }
}