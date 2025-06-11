using Microsoft.AspNetCore.Mvc;
using SuperSoccerShowdown.Application.Services;
using SuperSoccerShowdown.Application.Simulator;
using SuperSoccerShowdown.Application.Strategies;
using SuperSoccerShowdown.Domain.Entities;
using SuperSoccerShowdown.Domain.Enums;
using SuperSoccerShowdown.WebApi.Controllers;
using static SuperSoccerShowdown.Tests.TeamGeneratorServiceTests;

namespace SuperSoccerShowdown.Tests
{
    public class TeamsControllerTests
    {
        [Fact]
        public async Task GenerateTeam_GivenValidInput_ShouldReturnOk()
        {
            MockApiClientFactory mockService = new MockApiClientFactory();
            var strategy = new DefaultTeamFormationStrategy();
            var service = new TeamGeneratorService(mockService, strategy);
            var controller = new TeamsController(service, new MatchSimulator());

            var result = await controller.GenerateTeam("pokemon", 2, 2);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GenerateTeam_GivenInvalidInput_ShouldReturnBadRequest()
        {
            var mockService = new MockApiClientFactory();
            var strategy = new DefaultTeamFormationStrategy();
            var service = new TeamGeneratorService(mockService, strategy);
            var controller = new TeamsController(service, new MatchSimulator());

            var result = await controller.GenerateTeam("pokemon", 0, 4);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void SimulateMatch_GivenTwoTeams_ShouldReturnHighlights()
        {
            var controller = new TeamsController(null, new MatchSimulator());
            var teams = new List<Team>
            {
                new Team { Players = new List<Player> { new() { Name = "A", Position = PositionType.Goalie, SoccerPower = 90 }, new() { Name = "B", Position = PositionType.Offence, SoccerPower = 80 }, new() { Name = "C", Position = PositionType.Defence, SoccerPower = 70 }, new() { Name = "D", Position = PositionType.Defence, SoccerPower = 60 }, new() { Name = "E", Position = PositionType.Offence, SoccerPower = 85 } } },
                new Team { Players = new List<Player> { new() { Name = "A2", Position = PositionType.Goalie, SoccerPower = 90 }, new() { Name = "B2", Position = PositionType.Offence, SoccerPower = 80 }, new() { Name = "C2", Position = PositionType.Defence, SoccerPower = 70 }, new() { Name = "D2", Position = PositionType.Defence, SoccerPower = 60 }, new() { Name = "E2", Position = PositionType.Offence, SoccerPower = 85 } } }
            };

            var result = controller.SimulateMatch(teams);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void SimulateMatch_GivenInvalidTeamCount_ShouldReturnBadRequest()
        {
            var controller = new TeamsController(null, new MatchSimulator());
            var result = controller.SimulateMatch(new List<Team> { new Team() });

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}

