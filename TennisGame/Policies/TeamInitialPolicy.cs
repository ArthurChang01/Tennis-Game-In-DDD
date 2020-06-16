using System;
using System.Collections.Generic;
using System.Linq;
using TennisGame.Exceptions;

namespace TennisGame.Policies
{
    internal class TeamInitialPolicy
    {
        public (bool validateResult, Exception  exception) Validate(IEnumerable<Player> players, int score)
        {
            var exceptions = new List<Exception>();

            if(players?.Any() is null)
                exceptions.Add(new ArgumentException("Players can't be null or empty"));

            if(players?.Count() >2)
                exceptions.Add(new TeamInitialException("The amount of players should not bigger than 2", 
                    "amount of players", players.Count()));

            if (players.Count() == 2 && (players.First(), players.ElementAt(1)) switch
            {
                var (p1, p2) when p1.TeamId != p2.TeamId ||
                                  p1.Id == p2.Id => false,
                _ => true
            })  
                exceptions.Add(
                    new TeamInitialException("When player1 and player2 are in the same team, they should have the same Team Id, and different player Id", 
                        players.First().TeamId, players.ElementAt(1).TeamId, players.First().Id, players.ElementAt(1).Id));

            if(score < 0)
                exceptions.Add(new TeamInitialException("Score should be positive", "Score", score));

            return (exceptions.Count == 0, new AggregateException(exceptions));
        }
    }
}