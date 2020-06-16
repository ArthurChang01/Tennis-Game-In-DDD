using NUnit.Framework;

namespace TennisGame.Tests
{
    [TestFixture]
    public class WhenDeuce : TestClassBase
    {
        [SetUp]
        public void Initial()
        {
            SetGame();

            BothWin(3);
        }

        [Test]
        public void When_Player1_Win_Should_Team1Adv()
        {
            Win(Team1Id, 1);

            Assert($"{Team1Id} Adv");
        }

        [Test]
        public void When_Player1_WinTwice_Should_Team1Win()
        {
            Win(Team1Id, 2);

            Assert($"{Team1Id} Win");
        }

        [Test]
        public void When_Player2_Win_Should_Team2Adv()
        {
            Win(Team2Id, 1);

            Assert($"{Team2Id} Adv");
        }

        [Test]
        public void When_Player2_WinTwice_Should_Team2Win()
        {
            Win(Team2Id, 2);

            Assert($"{Team2Id} Win");
        }
    }
}