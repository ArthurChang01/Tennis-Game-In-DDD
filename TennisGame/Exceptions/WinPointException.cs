using System;
using TennisGame.Models;

namespace TennisGame.Exceptions
{
    public class WinPointException : Exception
    {
        #region Fields

        private readonly string _message;

        #endregion Fields

        #region Constructors

        public WinPointException(string message, GameStatus status)
        {
            _message = $"{message}, Game status: {status}";
        }

        public WinPointException(string message, string teamId)
        {
            _message = $"{message}, Team Id: {teamId}";
        }

        public WinPointException(string message, in int score)
        {
            _message = $"{message}, score: {score}";
        }

        #endregion Constructors

        public override string Message => _message;
    }
}