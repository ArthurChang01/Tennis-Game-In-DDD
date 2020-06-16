using System.Linq;
using TennisGame.Commands;

namespace TennisGame.Policies
{
    internal class LosePointPolicy
    {
        public bool Validate(LostPoint cmd, string team1Id, string team2Id, int score, GameStatus status)
        {
            return status != GameStatus.End &&
                   new[] { team1Id, team2Id }.Contains(cmd.TeamId) &&
                   score < 0;
        }
    }
}