using System;
using System.Collections.Generic;
using System.Linq;
using TennisGame.Commands;
using TennisGame.Exceptions;
using TennisGame.Models;

namespace TennisGame.Policies
{
    internal class LosePointPolicy
    {
        public (bool validateResult, Exception exception) Validate(LostPoint cmd, string team1Id, string team2Id, GameStatus status)
        {
            var exceptions = new List<Exception>();

            if (status == GameStatus.End)
                exceptions.Add(new LostPointException("Game status can't be end.", status));

            if (new[] { team1Id, team2Id }.Any(o => o == cmd.TeamId) == false)
                exceptions.Add(new LostPointException("The specific team Id is not exist.", cmd.TeamId));

            return (exceptions.Count == 0, new AggregateException(exceptions));
        }
    }
}