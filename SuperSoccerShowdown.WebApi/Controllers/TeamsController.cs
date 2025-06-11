using Microsoft.AspNetCore.Mvc;
using SuperSoccerShowdown.Application.Services;
using SuperSoccerShowdown.Application.Simulator;
using SuperSoccerShowdown.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace SuperSoccerShowdown.WebApi.Controllers
{
    public record MatchRequest(Team TeamA, Team TeamB);

    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly TeamGeneratorService _teamGenerator;
        private readonly MatchSimulator _simulator;

        public TeamsController(TeamGeneratorService teamGenerator, MatchSimulator simulator)
        {
            _teamGenerator = teamGenerator;
            _simulator = simulator;
        }

        /// <summary>
        /// Generates a team from a selected universe using the specified lineup.
        /// </summary>
        /// <param name="universe">The universe to select players from. Options: "starwars", "pokemon"</param>
        /// <param name="defenders">Number of defenders (heaviest players)</param>
        /// <param name="attackers">Number of attackers (shortest players)</param>
        [HttpGet("generate")]
        [SwaggerOperation(Summary = "Generate a soccer team from a universe.", Description = "Each team has exactly 5 players: 1 Goalie (tallest), specified number of Defenders (heaviest), and Attackers (shortest). Universe must be one of: 'starwars', 'pokemon'. Total defenders + attackers = 4.")]
        public async Task<IActionResult> GenerateTeam([Required][FromQuery] string universe,
                                                    [Required][Range(1, 3)][FromQuery] int defenders,
                                                    [Required][Range(1, 3)][FromQuery] int attackers)
        {
            try
            {
                var team = await _teamGenerator.GenerateTeamAsync(universe, defenders, attackers);
                return Ok(team);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Simulates a match between two generated teams.
        /// </summary>
        /// <param name="teams">List of two valid teams</param>
        /// <returns>List of highlight strings from the match</returns>
        [HttpPost("simulate")]
        [SwaggerOperation(Summary = " Simulates a match between two generated teams.", Description = " Match simulation based on TeamPower. List of highlight strings from the match")]
        public IActionResult SimulateMatch([FromBody] List<Team> teams)
        {
            if (teams.Count != 2) return BadRequest("Exactly two teams must be provided");

            var highlights = _simulator.Simulate(teams[0], teams[1]);
            return Ok(highlights);
        }
    }
}
