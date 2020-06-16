namespace TennisGame
{
    public class Player
    {
        public Player(string id, string name, string teamId = "")
        {
            Id = id;
            this.Name = name;
            TeamId = teamId;
        }

        public string Id { get; }
        public string Name { get; set; }
        public string TeamId { get; }
    }
}