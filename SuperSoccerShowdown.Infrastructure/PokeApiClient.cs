using Microsoft.Extensions.Caching.Memory;
using Polly;
using SuperSoccerShowdown.Application.Interfaces;
using SuperSoccerShowdown.Domain.Entities;
using System.Net.Http.Json;

namespace SuperSoccerShowdown.Infrastructure
{
    public class PokeApiClient : IUniverseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly AsyncPolicy<HttpResponseMessage> _resiliencePolicy;


        public PokeApiClient(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
            _resiliencePolicy = Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .OrResult(msg => !msg.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)))
                .WrapAsync(
                    Policy<HttpResponseMessage>
                        .Handle<Exception>()
                        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30))
                );
        }

        public async Task<List<Player>> GetRandomPlayersAsync(int count)
        {
            var players = new List<Player>();
            var rand = new Random();

            for (int i = 0; i < count; i++)
            {
                int id = rand.Next(1, 151);

                if (_cache.TryGetValue($"pokemon-{id}", out Player cached))
                {
                    players.Add(cached);
                    continue;
                }

                try
                {
                    var response = await _resiliencePolicy.ExecuteAsync(() =>
                        _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}/"));

                    if (!response.IsSuccessStatusCode) continue;

                    var pokemon = await response.Content.ReadFromJsonAsync<PokeData>();
                    if (pokemon != null)
                    {
                        var player = new Player
                        {
                            Name = pokemon.Name,
                            HeightCm = pokemon.Height * 10,
                            WeightKg = pokemon.Weight / 10
                        };

                        _cache.Set($"pokemon-{id}", player, TimeSpan.FromMinutes(10));
                        players.Add(player);
                    }
                }
                catch
                {
                    continue;
                }
            }

            return players;
        }

    }
}
