using Microsoft.AspNetCore.Mvc;
using StatsPerformChallengeAPI.Models.Interfaces;
using StatsPerformChallengeAPI.Models.TechChallenge;

namespace StatsPerformChallengeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaguesController(IProtectedDataAccess protectedDataAccess) : ControllerBase
    {
        private readonly IProtectedDataAccess _context = protectedDataAccess;

        /// <summary>
        /// Get target league options for dropdown
        /// </summary>
        /// <returns>Available league options</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<League>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<League> GetLeagues()
        {
            try
            {
                return Ok(_context.GetLeaguesAsync().Result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }

        }

        /// <summary>
        /// Get unbranded matches (for a selected league)
        /// </summary>
        /// <param name="leagueId">League for which the matches will be listed</param>
        /// <returns>Unbranded matches for league <paramref name="leagueId"/></returns>
        [HttpGet("{leagueId}/unbranded-matches")]
        [ProducesResponseType<IEnumerable<Match>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Match>> GetUnbrandedMatches(string leagueId)
        {
            try
            {
                return Ok(_context.GetMatchesAsync(leagueId).Result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// </summary>
        /// <param name="leagueId">League for which the matches will be listed</param>
        /// <param name="brandingId">Branding which will be applied to the matches</param>
        /// <returns>Unbranded matches for league <paramref name="leagueId"/></returns>
        [HttpGet("{leagueId}/branded-matches/{brandingId}")]
        [ProducesResponseType<IEnumerable<Match>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Match>> GetBrandedMatches(string leagueId, string brandingId)
        {
            try
            {
                return Ok(_context.GetMatchesAsync(leagueId, brandingId).Result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
