using SuperSoccerShowdown.Application.Interfaces;
using SuperSoccerShowdown.Application.Utils;
using SuperSoccerShowdown.Domain.Entities;
using SuperSoccerShowdown.Domain.Enums;


namespace SuperSoccerShowdown.Application.Strategies
{
    public class DefaultTeamFormationStrategy : ITeamFormationStrategy
    {
        // Tallest → Goalie, Heaviest → Defenders, Shortest → Attackers
        public Team FormTeam(string universe, List<Player> players, int defenders, int attackers)
        {
            if (players.Count != 5)
                throw new ArgumentException("A team must have exactly 5 players");

            if (defenders <= 0 || attackers <= 0)
                throw new ArgumentException("A team must have at least one attacker and one defender.");

            var sortedByHeight = players.OrderByDescending(p => p.HeightCm).ToList();
            var goalie = sortedByHeight.First();

            var others = sortedByHeight.Skip(1).ToList();
            var defendersList = others.OrderByDescending(p => p.WeightKg).Take(defenders).ToList();
            var attackersList = others.Except(defendersList).OrderBy(p => p.HeightCm).Take(attackers).ToList();

            if (defendersList.Count != defenders || attackersList.Count != attackers)
                throw new ArgumentException("Not enough valid players to fulfill lineup");

            var teamPlayers = new List<Player>();

            goalie.Position = PositionType.Goalie;
            goalie.SoccerPower = Random.Shared.Next(PowerRanges.GoalieMin, PowerRanges.GoalieMax);
            teamPlayers.Add(goalie);

            foreach (var d in defendersList)
            {
                d.Position = PositionType.Defence;
                d.SoccerPower = Random.Shared.Next(PowerRanges.DefenderMin, PowerRanges.DefenderMax);
                teamPlayers.Add(d);
            }

            foreach (var a in attackersList)
            {
                a.Position = PositionType.Offence;
                a.SoccerPower = Random.Shared.Next(PowerRanges.AttackerMin, PowerRanges.AttackerMax);
                teamPlayers.Add(a);
            }

            return new Team
            {
                Universe = universe,
                Players = teamPlayers
            };
        }
    }
}
