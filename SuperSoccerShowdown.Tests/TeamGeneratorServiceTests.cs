using SuperSoccerShowdown.Application.Interfaces;
using SuperSoccerShowdown.Application.Services;
using SuperSoccerShowdown.Application.Strategies;
using SuperSoccerShowdown.Domain.Entities;
using SuperSoccerShowdown.Domain.Enums;

namespace SuperSoccerShowdown.Tests
{

    public class TeamGeneratorServiceTests
    {
        private class MockApiClient : IUniverseApiClient
        {
            public Task<List<Player>> GetCharactersAsync(int count)
            {
                return Task.FromResult(new List<Player>
                {
                    new() { Name = "One", HeightCm = 180, WeightKg = 70 },
                    new() { Name = "Two", HeightCm = 175, WeightKg = 80 },
                    new() { Name = "Three", HeightCm = 170, WeightKg = 60 },
                    new() { Name = "Four", HeightCm = 165, WeightKg = 75 },
                    new() { Name = "Five", HeightCm = 160, WeightKg = 65 },
                });
            }

            public Task<List<Player>> GetRandomPlayersAsync(int count)
            {
                return GetCharactersAsync(count);
            }
        }

        public class MockApiClientFactory : IUniverseApiClientFactory
        {
            public IUniverseApiClient GetClient(string universe)
            {
                return universe switch
                {
                    "pokemon" => new MockApiClient(),
                    "starwars" => new MockApiClient(),
                    _ => throw new ArgumentException("Unknown universe: " + universe)
                };
            }
        }

        [Fact]
        public async Task GenerateTeamAsync_GivenValidInput_ShouldReturnValidTeam()
        {
            var strategy = new DefaultTeamFormationStrategy();
            var service = new TeamGeneratorService(new MockApiClientFactory(), strategy);

            var team = await service.GenerateTeamAsync("pokemon", 2, 2);

            Assert.Equal(5, team.Players.Count);
            Assert.Equal(1, team.Players.Count(p => p.Position == PositionType.Goalie));
            Assert.Equal(2, team.Players.Count(p => p.Position == PositionType.Defence));
            Assert.Equal(2, team.Players.Count(p => p.Position == PositionType.Offence));
        }

        [Fact]
        public async Task GenerateTeamAsync_GivenInvalidLineup_ShouldThrowException()
        {
            var strategy = new DefaultTeamFormationStrategy();
            var service = new TeamGeneratorService(new MockApiClientFactory(), strategy);

            await Assert.ThrowsAsync<ArgumentException>(() => service.GenerateTeamAsync("pokemon", 0, 4));
        }

        [Fact]
        public async void GetClient_GivenUnknownUniverse_ShouldThrowArgumentException()
        {
            var factory = new MockApiClientFactory();

           
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                var client = factory.GetClient("unknown");
                await client.GetRandomPlayersAsync(1);
            });
        }
    }
}