using System;
using TennisGame.Models;

namespace TennisGame.Exceptions
{
    public class GameInitialException : Exception
    {
        #region Fields

        private const string _message = "Game initial fail";

        #endregion Fields

        #region Properties

        public GameStatus ActualGameStatus { get; set; }

        public GameStatus ExpectedGameStatus { get; set; }

        public int Team1Score { get; set; }

        public int Team2Score { get; set; }

        #endregion Properties

        public override string Message
            => $@"{_message}.
               Team1 Score: {Team1Score},
               Team2 Score: {Team2Score},
               Expected Status: {ExpectedGameStatus},
               Actual Status: {ActualGameStatus}";
    }
}