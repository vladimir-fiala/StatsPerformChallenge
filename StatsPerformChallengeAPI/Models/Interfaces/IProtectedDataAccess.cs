using StatsPerformChallengeAPI.Models.TechChallenge;

namespace StatsPerformChallengeAPI.Models.Interfaces
{
    public interface IProtectedDataAccess
    {
        public Task<IEnumerable<BrandingDefinition>> GetBrandingsAsync();
        public Task<IEnumerable<Branding>> GetBrandingByIdAsync(string brandingId);
        public Task<IEnumerable<League>> GetLeaguesAsync();
        public Task<IEnumerable<Match>> GetMatchesAsync(string leagueId);
        public Task<IEnumerable<Match>> GetMatchesAsync(string leagueId, string brandingId);
    }
}
