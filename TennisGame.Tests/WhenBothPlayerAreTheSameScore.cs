using NUnit.Framework;

namespace TennisGame.Tests
{
    [TestFixture]
    public class WhenBothPlayerAreTheSameScore : TestClassBase
    {
        [Test]
        public void When_ScoreIs0_Should_LoveAll()
        {
            SetGame();

            Assert("Love - All");
        }

        [Test]
        public void When_ScoreIs1_Should_FifteenAll()
        {
            SetGame();

            BothWin(1);

            Assert("Fifteen - All");
        }

        [Test]
        public void When_ScoreIs2_Should_ThirtyAll()
        {
            SetGame();

            BothWin(2);

            Assert("Thirty - All");
        }

        [Test]
        public void When_ScoreIs3_Should_Deuce()
        {
            SetGame();

            BothWin(3);

            Assert("Deuce");
        }

        [Test]
        public void When_ScoreIs4_Should_Deuce()
        {
            SetGame();

            BothWin(4);

            Assert("Deuce");
        }
    }
}