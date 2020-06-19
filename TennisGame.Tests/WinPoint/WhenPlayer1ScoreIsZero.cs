using NUnit.Framework;

namespace TennisGame.Tests.WinPoint
{
    [TestFixture]
    public class WhenPlayer1ScoreIsZero : TestClassBase
    {
        [Test]
        public void When_Player2ScoreIs1_Should_FifteenLove()
        {
            SetGame();

            Win(Team2Id, 1);

            Assert("Love - Fifteen");
        }

        [Test]
        public void When_Player2ScoreIs2_Should_ThirtyLove()
        {
            SetGame();

            Win(Team2Id, 2);

            Assert("Love - Thirty");
        }

        [Test]
        public void When_Player2ScoreIs3_Should_FortyLove()
        {
            SetGame();

            Win(Team2Id, 3);

            Assert("Love - Forty");
        }
    }
}