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
    public class WinPointEventConverterTest
    {
        [Test]
        public void Serialize()
        {
            var le = new WinPointEvent("1", 1, DateTimeOffset.Now, "tid", "pid", "score", GameStatus.End);
            var opt = new JsonSerializerOptions();
            opt.Converters.Add(new GameIdConverter());
            opt.Converters.Add(new PlayersConverter());
            opt.Converters.Add(new TeamConverter());
            opt.Converters.Add(new WinPointEventConverter());

            var actual = JsonSerializer.Serialize(le, opt);

            actual.Should().Be($"{{\"Id\":\"1\",\"Version\":1,\"OccuredDate\":\"{le.OccuredDate:yyyy/MM/dd HH:mm:ss}\",\"TeamId\":\"tid\",\"PlayerId\":\"pid\",\"NewScore\":\"score\",\"NewStatus\":\"End\"}}");
        }

        [Test]
        public void Deserialize()
        {
            var le = new WinPointEvent("1", 1, DateTimeOffset.Now, "tid", "pid", "score", GameStatus.End);
            var opt = new JsonSerializerOptions();
            opt.Converters.Add(new GameIdConverter());
            opt.Converters.Add(new PlayersConverter());
            opt.Converters.Add(new TeamConverter());
            opt.Converters.Add(new WinPointEventConverter());

            var str = JsonSerializer.Serialize(le, opt);
            var actual = JsonSerializer.Deserialize<WinPointEvent>(str, opt);

            actual.Should().Be(le);
        }
    }
}