using SuperSoccerShowdown.Domain.Enums;

namespace SuperSoccerShowdown.Domain.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public double HeightCm { get; set; }
        public double WeightKg { get; set; }
        public double SoccerPower { get; set; } // Custom metric for team power
        public PositionType Position { get; set; }
    }
}
