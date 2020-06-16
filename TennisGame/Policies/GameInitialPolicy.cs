using System;

namespace TennisGame.Policies
{
    internal class GameInitialPolicy
    {
        public bool Validate(int score1, int score2, GameStatus gameStatus)
        {
            var status = (score1, score2) switch
            {
                (0, 0) => GameStatus.Start,
                _ when (score1 >= 4 || score2 >= 4) && Math.Abs(score1 - score2) == 2 => GameStatus.End,
                _ => GameStatus.Playing
            };

            return gameStatus == status;
        }
    }
}