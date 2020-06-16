using System;
using System.Collections.Generic;
using System.Linq;
using TennisGame.Commands;
using TennisGame.Exceptions;

namespace TennisGame.Policies
{
    internal class LosePointPolicy
    {
        public (bool validateResult, Exception exception) Validate(LostPoint cmd, string team1Id, string team2Id, int score, GameStatus status)
        {
            var exceptions = new List<Exception>();

            if (status == GameStatus.End)
                exceptions.Add(new LostPointException("Game status can't be end.", status));

            if (new[] { team1Id, team2Id }.Any(o => o == cmd.TeamId))
                exceptions.Add(new LostPointException("The specific team Id is not exist.", cmd.TeamId));

            if (score > 0)
                exceptions.Add(new LostPointException("Score should be negative.", score));

            return (exceptions.Count == 0,  new AggregateException(exceptions));
        }
    }
}