namespace TennisGame.Models
{
    public class Player
    {
        #region Constructors

        public Player(string id, string name, string teamId = "")
        {
            Id = id;
            Name = name;
            TeamId = teamId;
        }

        #endregion Constructors

        #region Properties

        public string Id { get; }
        public string Name { get; set; }
        public string TeamId { get; }

        #endregion Properties
    }
}