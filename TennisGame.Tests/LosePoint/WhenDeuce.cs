using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TennisGame.Tests.LosePoint
{
    [TestFixture]
    public class WhenDeuce : TestClassBase
    {
        [Test]
        public void When_Player1ScoreIs1_And_Player2ScoreIs1_And_Player1Lose_Should_LoveFifteen()
        {
            SetGame(1, 1);

            Lose(Team1Id);

            Assert("Love - Fifteen");
        }

        [Test]
        public void When_Player1ScoreIs1_And_Player2ScoreIs1_And_Player2Lose_Should_FifteenLove()
        {
            SetGame(1, 1);

            Lose(Team2Id);

            Assert("Fifteen - Love");
        }

        [Test]
        public void When_Player1ScoreIs2_And_Player2ScoreIs2_And_Player1Lose_Should_FifteenThirty()
        {
            SetGame(2, 2);

            Lose(Team1Id);

            Assert("Fifteen - Thirty");
        }

        [Test]
        public void When_Player1ScoreIs2_And_Player2ScoreIs2_And_Player2Lose_Should_ThirtyFifteen()
        {
            SetGame(2, 2);

            Lose(Team2Id);

            Assert("Thirty - Fifteen");
        }

        [Test]
        public void When_Player1ScoreIs3_And_Player2ScoreIs3_And_Player1Lose_Should_ThirtyForty()
        {
            SetGame(3, 3);

            Lose(Team1Id);

            Assert("Thirty - Forty");
        }

        [Test]
        public void When_Player1ScoreIs3_And_Player2ScoreIs3_And_Player2Lose_Should_FortyThirty()
        {
            SetGame(3, 3);

            Lose(Team2Id);

            Assert("Forty - Thirty");
        }

        [Test]
        public void When_Player1ScoreIs4_And_Player2ScoreIs4_And_Player1Lose_Should_Team2Adv()
        {
            SetGame(4, 4);

            Lose(Team1Id);

            Assert($"{Team2Id} Adv");
        }

        [Test]
        public void When_Player1ScoreIs4_And_Player2ScoreIs4_And_Player2Lose_Should_Team1Adv()
        {
            SetGame(4, 4);

            Lose(Team2Id);

            Assert($"{Team1Id} Adv");
        }
    }
}