using Microsoft.AspNetCore.Mvc;
using StatsPerformChallengeAPI.Models.Interfaces;
using StatsPerformChallengeAPI.Models.TechChallenge;

namespace StatsPerformChallengeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandingsController(IProtectedDataAccess protectedDataAccess) : ControllerBase
    {
        private readonly IProtectedDataAccess _context = protectedDataAccess;

        /// <summary>
        /// Get branding options for dropdown
        /// </summary>
        /// <returns>Available branding options</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<BrandingDefinition>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BrandingDefinition>> GetBrandings()
        {
            try
            {
                return Ok(_context.GetBrandingsAsync().Result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}
