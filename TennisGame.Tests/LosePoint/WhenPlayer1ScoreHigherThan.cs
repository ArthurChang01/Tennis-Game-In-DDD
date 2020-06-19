using NUnit.Framework;

namespace TennisGame.Tests.LosePoint
{
    [TestFixture]
    public class WhenPlayer1ScoreHigherThan : TestClassBase
    {
        [Test]
        public void When_Player1ScoreIs1_And_Player2ScoreIs0_Should_LoveAll()
        {
            SetGame(1, 0);

            Lose(Team1Id);

            Assert("Love - All");
        }

        [Test]
        public void When_Player1ScoreIs2_And_Player2ScoreIs0_Should_FifteenLove()
        {
            SetGame(2, 0);

            Lose(Team1Id);

            Assert("Fifteen - Love");
        }

        [Test]
        public void When_Player1ScoreIs3_And_Player2ScoreIs0_Should_ThirtyLove()
        {
            SetGame(3, 0);

            Lose(Team1Id);

            Assert("Thirty - Love");
        }

        [Test]
        public void When_Player1ScoreIs3_And_Player2ScoreIs1_Should_ThirtyFifteen()
        {
            SetGame(3, 1);

            Lose(Team1Id);

            Assert("Thirty - Fifteen");
        }

        [Test]
        public void When_Player1ScoreIs3_And_Player2ScoreIs2_Should_ThirtyAll()
        {
            SetGame(3, 2);

            Lose(Team1Id);

            Assert("Thirty - All");
        }

        [Test]
        public void When_Player1Score4_And_Player2ScoreIs3_Should_Deuce()
        {
            SetGame(4, 3);

            Lose(Team1Id);

            Assert("Deuce");
        }
    }
}