using System;
using System.Collections.Generic;
using System.Linq;
using TennisGame.Commands;
using TennisGame.Exceptions;

namespace TennisGame.Policies
{
    internal class WinPointPolicy
    {
        public (bool validateResult, Exception exception) Validate(WinPoint cmd, string team1Id, string team2Id, int score, GameStatus status)
        {
            var exceptions = new List<Exception >();

            if(status == GameStatus.End)
                exceptions.Add(new WinPointException("Game status has been End.", status));

            if(new [] {team1Id, team2Id}.Any(o => o == cmd.TeamId) == false)
                exceptions.Add(new WinPointException("The specific team id is not exist.", cmd.TeamId));

            if(score < 0)
                exceptions.Add(new WinPointException("The score should be positive.", score));

            return (exceptions.Count == 0, new AggregateException(exceptions));
        }
    }
}