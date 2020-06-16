using NUnit.Framework;

namespace TennisGame.Tests
{
    [TestFixture]
    public class WhenPlayer2ScoreIsZero : TestClassBase
    {
        [Test]
        public void Should_LoveAll_When_Both0()
        {
            SetGame();

            Assert("Love - All");
        }

        [Test]
        public void When_Player1ScoreIs1_Should_FifteenLove()
        {
            SetGame();

            Win(Team1Id, 1);

            Assert("Fifteen - Love");
        }

        [Test]
        public void When_Player1ScoreIs2_Should_ThirtyLove()
        {
            SetGame();

            Win(Team1Id, 2);

            Assert("Thirty - Love");
        }

        [Test]
        public void When_Player1ScoreIs3_Should_FortyLove()
        {
            SetGame();

            Win(Team1Id, 3);

            Assert("Forty - Love");
        }
    }
}