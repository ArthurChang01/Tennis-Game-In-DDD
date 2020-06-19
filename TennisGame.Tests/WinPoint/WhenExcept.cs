using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TennisGame.Tests.WinPoint
{
    [TestFixture]
    public class WhenExcept : TestClassBase
    {
        [Test]
        public void When_Player1ScoreIs4_And_Player2ScoreIs0_Should_ThrowException()
        {
            SetGame(4, 0, GameStatus.End);

            WinForExcept(Team1Id);

            AssertException();
        }
    }
}