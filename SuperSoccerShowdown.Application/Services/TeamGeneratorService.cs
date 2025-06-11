using SuperSoccerShowdown.Application.Interfaces;
using SuperSoccerShowdown.Domain.Entities;

namespace SuperSoccerShowdown.Application.Services
{
    public class TeamGeneratorService
    {

        private readonly IUniverseApiClientFactory _factory;
        private readonly ITeamFormationStrategy _strategy;

        public TeamGeneratorService(IUniverseApiClientFactory factory, ITeamFormationStrategy strategy)
        {
            _factory = factory;
            _strategy = strategy;
        }

        public async Task<Team> GenerateTeamAsync(string universe, int defenders, int attackers)
        {
            if (defenders + attackers != 4)
                throw new ArgumentException("Team must have 1 goalie, plus a combination of 4 defenders and attackers");

            var client = _factory.GetClient(universe);
            var players = await client.GetRandomPlayersAsync(5);
            var team = _strategy.FormTeam(universe, players, defenders, attackers);
            return team;
        }
    }
}
