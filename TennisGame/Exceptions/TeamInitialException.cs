using System;

namespace TennisGame.Exceptions
{
    public class TeamInitialException : Exception
    {
        #region Fields

        private readonly string _message;

        #endregion Fields

        #region Constructors

        public TeamInitialException(string message, string entry, int count)
        {
            _message = $"{message}, the {entry} is : {count}";
        }

        public TeamInitialException(string message, string player1TeamId, string player2TeamId, string player1Id, string player2Id)
        {
            _message = $@"{message},
Player1's team id: {player1TeamId},
Player1's id: {player1Id},
Player2's team id: {player2TeamId},
Player2's id: {player2Id}";
        }

        #endregion Constructors

        public override string Message => _message;
    }
}