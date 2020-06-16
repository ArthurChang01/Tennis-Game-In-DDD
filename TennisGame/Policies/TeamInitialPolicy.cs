using System.Collections.Generic;
using System.Linq;

namespace TennisGame.Policies
{
    public class TeamInitialPolicy
    {
        public bool Validate(IEnumerable<Player> players, int score)
        {
            return (players != null && players.Any() && players.Count() <= 2) ||
                   (players.Count() == 2 && (players.First(), players.ElementAt(1)) switch
                   {
                       var (player1, player2) when player1.TeamId == player2.TeamId &&
                                                   player1.Id != player2.Id => true,
                       _ => false
                   }) &&
                score >= 0;
        }
    }
}