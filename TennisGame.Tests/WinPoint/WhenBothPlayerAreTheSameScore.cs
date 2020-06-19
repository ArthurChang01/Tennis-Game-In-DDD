using NUnit.Framework;

namespace TennisGame.Tests.WinPoint
{
    [TestFixture]
    public class WhenBothPlayerAreTheSameScore : TestClassBase
    {
        [Test]
        public void When_BothAre0_Should_LoveAll()
        {
            SetGame();

            Assert("Love - All");
        }

        [Test]
        public void When_BothAre1_Should_FifteenAll()
        {
            SetGame();

            BothWin(1);

            Assert("Fifteen - All");
        }

        [Test]
        public void When_BothAre2_Should_ThirtyAll()
        {
            SetGame();

            BothWin(2);

            Assert("Thirty - All");
        }

        [Test]
        public void When_BothAre3_Should_Deuce()
        {
            SetGame();

            BothWin(3);

            Assert("Deuce");
        }

        [Test]
        public void When_BothAre4_Should_Deuce()
        {
            SetGame();

            BothWin(4);

            Assert("Deuce");
        }

        [Test]
        public void When_BothAre8_Should_Deuce()
        {
            SetGame();

            BothWin(8);

            Assert("Deuce");
        }
    }
}