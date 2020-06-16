﻿using System;

namespace TennisGame.Exceptions
{
    public class WinPointException : Exception
    {
        private readonly string _message;

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

        public override string Message => _message;
    }
}