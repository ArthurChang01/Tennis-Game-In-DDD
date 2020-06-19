using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TennisGame.Tests.LosePoint
{
    [TestFixture]
    public class WhenPlayer2ScoreHigherThan : TestClassBase
    {
        [Test]
        public void When_Player1ScoreIs0_And_Player2ScoreIs1_Should_LoveAll()
        {
            SetGame(0, 1);

            Lose(Team2Id);

            Assert("Love - All");
        }

        [Test]
        public void When_Player1ScoreIs0_And_Player2ScoreIs2_Should_LoveFifteen()
        {
            SetGame(0, 2);

            Lose(Team2Id);

            Assert("Love - Fifteen");
        }

        [Test]
        public void When_Player1ScoreIs0_And_Player2ScoreIs3_Should_LoveThirty()
        {
            SetGame(0, 3);

            Lose(Team2Id);

            Assert("Love - Thirty");
        }

        [Test]
        public void When_Player1ScoreIs1_And_Player2ScoreIs2_Should_FifteenAll()
        {
            SetGame(1, 2);

            Lose(Team2Id);

            Assert("Fifteen - All");
        }

        [Test]
        public void When_Player1ScoreIs1_And_Player2ScoreIs3_Should_FifteenThirty()
        {
            SetGame(1, 3);

            Lose(Team2Id);

            Assert("Fifteen - Thirty");
        }

        [Test]
        public void When_Player1ScoreIs2_And_Player2ScoreIs3_Should_ThirtyAll()
        {
            SetGame(2, 3);

            Lose(Team2Id);

            Assert("Thirty - All");
        }

        [Test]
        public void When_Player1ScoreIs3_And_Player2ScoreIs4_Should_Deuce()
        {
            SetGame(3, 4);

            Lose(Team2Id);

            Assert("Deuce");
        }
    }
}