using SuperSoccerShowdown.Application.Interfaces;

namespace SuperSoccerShowdown.Infrastructure
{
    public class UniverseApiClientFactory : IUniverseApiClientFactory
    {
        private  SwapiClient _swapiClient;
        private PokeApiClient _pokeApiClient;

        public UniverseApiClientFactory(SwapiClient swapiClient, PokeApiClient pokeApiClient)
        {
            _swapiClient = swapiClient;
            _pokeApiClient = pokeApiClient;
        }

        public IUniverseApiClient GetClient(string universe) => universe.ToLower() switch
        {
            "starwars" => _swapiClient,
            "pokemon" => _pokeApiClient,
            _ => throw new ArgumentException("Invalid universe")
        };
    }
}
