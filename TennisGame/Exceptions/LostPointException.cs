using System;

namespace TennisGame.Exceptions
{
    public class LostPointException : Exception
    {
        private readonly string _message;

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

        public override string Message => _message;
    }
}