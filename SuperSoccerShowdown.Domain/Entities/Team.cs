
namespace SuperSoccerShowdown.Domain.Entities
{
    public class Team
    {
        public string Universe { get; set; }
        public List<Player> Players { get; set; } = new();
        public double TeamPower => Players.Sum(p => p.SoccerPower);
    }

}
