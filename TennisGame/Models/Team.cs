using System;
using System.Linq;
using TennisGame.Policies;

namespace TennisGame.Models
{
    public class Team
    {
        #region Fields

        private readonly Player[] _players;

        #endregion Fields

        #region Constructors

        public Team()
        {
            Id = Guid.NewGuid().ToString();
            Name = Id;
            _players = new[]
            {
                new Player(Guid.NewGuid().ToString(), "player1", Id),
                new Player(Guid.NewGuid().ToString(), "player2", Id)
            };
        }

        public Team(string id = "", string name = "", int score = 0, params Player[] players)
        {
            Id = id;
            Name = string.IsNullOrWhiteSpace(name) ? id : name;
            if (new TeamInitialPolicy().Validate(players, score) is (bool validateResult, Exception exception) &&
                 validateResult == false)
                throw exception;

            _players = players.ToArray();
            Score = score;
        }

        public Team(params Player[] players)
            : this(Guid.NewGuid().ToString(), "", 0, players)
        {
        }

        #endregion Constructors

        #region Properties

        public string Id { get; private set; }
        public string Name { get; }

        public int Score { get; private set; } = 0;

        #endregion Properties

        #region Public Methods

        public void AddPoint()
        {
            Score += 1;
        }

        public void Deduction()
        {
            if (Score == 0)
                return;

            Score -= 1;
        }

        public bool ExistPlayer(string playerId)
            => _players.Any(p => p.Id.Equals(playerId));

        #endregion Public Methods
    }
}