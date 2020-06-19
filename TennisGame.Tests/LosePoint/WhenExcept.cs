using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TennisGame.Tests.LosePoint
{
    [TestFixture]
    public class WhenExcept : TestClassBase
    {
        [Test]
        public void When_Pass_GameStatusIsEnd_Should_ThrowException()
        {
            SetGame(4, 0, GameStatus.End);

            LoseForException(Team1Id);

            AssertException();
        }
    }
}