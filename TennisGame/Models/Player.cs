namespace TennisGame.Models
{
    public class Player
    {
        public Player(string id, string name, string teamId = "")
        {
            Id = id;
            Name = name;
            TeamId = teamId;
        }

        public string Id { get; }
        public string Name { get; set; }
        public string TeamId { get; }
    }
}