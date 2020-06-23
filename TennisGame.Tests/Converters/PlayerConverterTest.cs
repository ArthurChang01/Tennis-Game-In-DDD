using System.Text.Json;
using FluentAssertions;
using NUnit.Framework;
using TennisGame.Applications.Serializers.JsonConverters;
using TennisGame.Models;

namespace TennisGame.Tests.Converters
{
    [TestFixture]
    public class PlayerConverterTest
    {
        [Test]
        public void Serialize()
        {
            var player = new Player("1", "player", "team");
            var opt = new JsonSerializerOptions();
            opt.Converters.Add(new PlayersConverter());

            var actual = JsonSerializer.Serialize(player, opt);

            actual.Should().Be($"{{\"{nameof(player.Id)}\":\"{player.Id}\",\"{nameof(player.Name)}\":\"{player.Name}\",\"{nameof(player.TeamId)}\":\"{player.TeamId}\"}}");
        }

        [Test]
        public void Deserialize()
        {
            var player = new Player("1", "player1", "team");
            var opt = new JsonSerializerOptions();
            opt.Converters.Add(new PlayersConverter());

            var str = JsonSerializer.Serialize(player, opt);
            var actual = JsonSerializer.Deserialize<Player>(str);

            actual.Id.Should().Be(actual.Id);
            actual.Name.Should().Be(actual.Name);
            actual.TeamId.Should().Be(actual.TeamId);
        }
    }
}