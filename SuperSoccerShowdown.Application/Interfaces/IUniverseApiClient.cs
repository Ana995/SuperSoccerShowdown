using SuperSoccerShowdown.Domain.Entities;

namespace SuperSoccerShowdown.Application.Interfaces
{
    public interface IUniverseApiClient
    {
        Task<List<Player>> GetRandomPlayersAsync(int count);
    }

}
