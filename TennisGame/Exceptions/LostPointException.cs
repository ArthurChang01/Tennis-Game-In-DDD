using System;
using TennisGame.Models;

namespace TennisGame.Exceptions
{
    public class LostPointException : Exception
    {
        #region Fields

        private readonly string _message;

        #endregion Fields

        #region Constructors

        public LostPointException(string message, GameStatus status)
        {
            _message = $"{message}, currently game status is {status}";
        }

        public LostPointException(string message, string teamId)
        {
            _message = $"{message}, currently team Id is {teamId}";
        }

        public LostPointException(string message, in int score)
        {
            _message = $"{message}, currently score is {score}";
        }

        #endregion Constructors

        public override string Message => _message;
    }
}