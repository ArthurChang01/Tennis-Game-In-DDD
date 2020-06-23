using System;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TennisGame.Applications.Serializers;
using TennisGame.Events;
using TennisGame.Models;

namespace TennisGame.Tests.Converters
{
    [TestFixture]
    public class ESerializer
    {
        private GameSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new GameSerializer();
        }

        [Test]
        public void Serialize_GameInitialEvent()
        {
            var ge = new GameInitialEvent(Guid.NewGuid().ToString(), 1, DateTimeOffset.Now, new GameId(), new[] { new Team("1", "team", 0, new[] { new Player("1", "player", "1"), }), }, GameStatus.Start, "score");

            var actual = _serializer.Convert(ge);

            Encoding.UTF8.GetString(actual.Data)
                .Should().Be($"{{\"Id\":\"{ge.Id}\",\"Version\":1,\"OccuredDate\":\"{ge.OccuredDate:yyyy/MM/dd HH:mm:ss}\",\"GameId\":{{\"GameId\":\"{ge.GameId}\"}},\"Teams\":[{{\"Id\":\"1\",\"Name\":\"team\",\"Players\":[{{\"Id\":\"1\",\"Name\":\"player\",\"TeamId\":\"1\"}}],\"Score\":0}}],\"Status\":\"Start\",\"Score\":\"score\"}}");
        }

        [Test]
        public void Serialize_WinPointEvent()
        {
            var wpe = new WinPointEvent(Guid.NewGuid().ToString(), 1, DateTimeOffset.Now, "tid", "pid", "score", GameStatus.End);

            var actual = _serializer.Convert(wpe);

            Encoding.UTF8.GetString(actual.Data)
                .Should().Be($"{{\"Id\":\"{wpe.Id}\",\"Version\":1,\"OccuredDate\":\"{wpe.OccuredDate:yyyy/MM/dd HH:mm:ss}\",\"TeamId\":\"tid\",\"PlayerId\":\"pid\",\"NewScore\":\"score\",\"NewStatus\":\"End\"}}");
        }

        [Test]
        public void Serialize_LosePointEvent()
        {
            var wpe = new LosePointEvent(Guid.NewGuid().ToString(), 1, DateTimeOffset.Now, "tid", "pid", "score", GameStatus.End);

            var actual = _serializer.Convert(wpe);

            Encoding.UTF8.GetString(actual.Data)
                .Should().Be($"{{\"Id\":\"{wpe.Id}\",\"Version\":1,\"OccuredDate\":\"{wpe.OccuredDate:yyyy/MM/dd HH:mm:ss}\",\"TeamId\":\"tid\",\"PlayerId\":\"pid\",\"NewScore\":\"score\",\"NewStatus\":\"End\"}}");
        }
    }
}