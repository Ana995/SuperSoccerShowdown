using SuperSoccerShowdown.Domain.Entities;
using SuperSoccerShowdown.Domain.Enums;
using System.Data;


namespace SuperSoccerShowdown.Application.Simulator
{
    public class MatchSimulator
    {
        public MatchResult Simulate(Team teamA, Team teamB)
        {
            var rndP = new Random();
            var highlights = new List<MatchHighlight>();

            var aAttackers = teamA.Players.Where(p => p.Position == PositionType.Offence).ToList();
            var bGoalie = teamB.Players.FirstOrDefault(p => p.Position == PositionType.Goalie);

            foreach (var atk in aAttackers)
            {
                if (rndP.NextDouble() < atk.SoccerPower / (atk.SoccerPower + (bGoalie?.SoccerPower ?? 50)))
                    highlights.Add(new MatchHighlight { Description = $"{atk.Name} scored against {bGoalie?.Name}" });
            }

            var bAttackers = teamB.Players.Where(p => p.Position == PositionType.Offence).ToList();
            var aGoalie = teamA.Players.FirstOrDefault(p => p.Position == PositionType.Goalie);

            foreach (var atk in bAttackers)
            {
                if (rndP.NextDouble() < atk.SoccerPower / (atk.SoccerPower + (aGoalie?.SoccerPower ?? 50)))
                    highlights.Add(new MatchHighlight { Description = $"{atk.Name} scored against {aGoalie?.Name}" });
            }

            return new MatchResult { TeamA = teamA, TeamB = teamB, Highlights = highlights };
        }

    }

}
