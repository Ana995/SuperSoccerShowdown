namespace SuperSoccerShowdown.Domain.Entities
{
    public class MatchResult
    {
        public Team TeamA { get; set; }
        public Team TeamB { get; set; }
        public List<MatchHighlight> Highlights { get; set; } 
    }
}
