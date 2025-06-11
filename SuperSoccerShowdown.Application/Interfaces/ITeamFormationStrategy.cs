
using SuperSoccerShowdown.Domain.Entities;

namespace SuperSoccerShowdown.Application.Interfaces
{
    public interface ITeamFormationStrategy
    {
        Team FormTeam(string universe, List<Player> players, int defenders, int attackers);
    }
}
