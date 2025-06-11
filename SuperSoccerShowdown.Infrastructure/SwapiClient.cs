using SuperSoccerShowdown.Application.Interfaces;
using SuperSoccerShowdown.Domain.Entities;
using System.Net.Http.Json;

namespace SuperSoccerShowdown.Infrastructure
{
    public class SwapiClient : IUniverseApiClient
    {
        private readonly HttpClient _httpClient;

        public SwapiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Player>> GetRandomPlayersAsync(int count)
        {
            var players = new List<Player>();
            var rand = new Random();

            for (int i = 0; i < count; i++)
            {
                int id = rand.Next(1, 83);
                try
                {
                    var character = await _httpClient.GetFromJsonAsync<SwapiCharacter>($"https://swapi.dev/api/people/{id}/");
                    if (character != null && double.TryParse(character.Height, out var h) && double.TryParse(character.Mass, out var w))
                    {
                        players.Add(new Player
                        {
                            Name = character.Name,
                            HeightCm = h,
                            WeightKg = w
                        });
                    }
                }
                catch { continue; }
            }

            return players;
        }
    }
}
