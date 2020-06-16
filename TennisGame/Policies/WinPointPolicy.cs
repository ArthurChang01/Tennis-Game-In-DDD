using System.Linq;
using TennisGame.Commands;

namespace TennisGame.Policies
{
    internal class WinPointPolicy
    {
        public bool Validate(WinPoint cmd, string team1Id, string team2Id, int score, GameStatus status)
        {
            return status != GameStatus.End &&
                   new[] { team1Id, team2Id }.Any(o => o == cmd.TeamId) &&
                   score > 0;
        }
    }
}