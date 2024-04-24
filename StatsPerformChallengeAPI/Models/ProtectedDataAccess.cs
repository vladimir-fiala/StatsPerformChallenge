using AutoMapper;
using Microsoft.Extensions.Options;
using StatsPerformChallengeAPI.Exceptions;
using StatsPerformChallengeAPI.Extensions;
using StatsPerformChallengeAPI.Models.Interfaces;
using StatsPerformChallengeAPI.Models.StatsPerformDTOv1;
using StatsPerformChallengeAPI.Models.TechChallenge;
using System.Net.Http.Headers;

namespace StatsPerformChallengeAPI.Models
{
    public class ProtectedDataAccess(ILogger<ProtectedDataAccess> logger, IOptions<AppSettings> settings, IMapper mapper) : IProtectedDataAccess
    {
        private readonly ILogger<ProtectedDataAccess> _logger = logger;
        private readonly AppSettings _settings = settings.Value;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<BrandingDefinition>> GetBrandingsAsync()
        {
            var brandDefinitionsDTO = await GetProtectedDataFileAsync<BrandingDefinitionDTO>(_settings.BrandingsUrl);
            return _mapper.Map<List<BrandingDefinition>>(brandDefinitionsDTO);
        }

        public async Task<IEnumerable<Branding>> GetBrandingByIdAsync(string brandingId)
        {
            string url = string.Format(_settings.BrandingDetailsUrl, brandingId);
            var brandDetailsDTO = await GetProtectedDataFileAsync<BrandingDetailsDTO>(url);
            return _mapper.Map<List<Branding>>(brandDetailsDTO);
        }

        public async Task<IEnumerable<League>> GetLeaguesAsync()
        {
            var leaguesDTO = await GetProtectedDataFileAsync<LeagueDTO>(_settings.LeaguesUrl);
            return _mapper.Map<List<League>>(leaguesDTO);
        }

        public async Task<IEnumerable<Match>> GetMatchesAsync(string leagueId)
        {
            // I would REALLY like some kind of input validation here, but we don't really have enough information to do that.
            // (e.g. parsing string into into an int, then checking it's greater than 0)?
            string url = string.Format(_settings.MatchesUrl, leagueId);
            var matchesDTO = await GetProtectedDataFileAsync<MatchDTO>(url);
            var matchesBO = _mapper.Map<List<Match>>(matchesDTO);
            return matchesBO;
        }

        public async Task<IEnumerable<Match>> GetMatchesAsync(string leagueId, string brandId)
        {
            // We don't need to wait for matches to load before we start loading branding, let's save some time
            var matchesTask = GetMatchesAsync(leagueId);
            var brandTask = GetBrandingByIdAsync(brandId);

            // We DO need the values before we can operate on them though
            await Task.WhenAll(matchesTask, brandTask);
            var matchesDTO = matchesTask.Result;
            var brandsDTO = brandTask.Result;

            // Let's use the AutoMapper library to convert between classes designed for StatsPerform API and our business logic model
            var matchesBO = _mapper.Map<List<Match>>(matchesDTO);
            var brandsBO = _mapper.Map<List<Branding>>(brandsDTO);

            if (matchesBO != null && brandsBO != null)
            {
                return await Match.CustomizeMatchesByBranding(matchesBO, brandsBO);
            }
            else
            {
                _logger.LogError("GetMatchesAsync(str, str) error: mapping resulted in null collection");
                throw new AutoMapperMappingException("Mapping resulted in null collection");
            }
        }

        private async Task<IEnumerable<T>> GetProtectedDataFileAsync<T>(string url)
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri(url.AppendSasToken());
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            string urlParameters = string.Empty;
            var response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                // Simulated long load, can be used to verify asynchronous calls
                // await Task.Delay(3000); 
                var parsedResponse = await response.Content.ReadFromJsonAsync<T[]>();
                if (parsedResponse != null)
                {
                    return parsedResponse;
                }
                else
                {
                    _logger.LogError("Error during ReadFromJsonAsync parsing into class {className}", nameof(T));
                    throw new ParsingException("Values could not be parsed.");
                }
            }
            else
            {
                _logger.LogError("GET {endpointUrl} encountered an issue {statusCode} ({reasonPhrase})", url, response.StatusCode, response.ReasonPhrase);
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    if (typeof(T) == typeof(MatchDTO))
                    {
                        throw new ArgumentException("Specified league does not exist!");
                    }
                    if (typeof(T) == typeof(BrandingDetailsDTO))
                    {
                        throw new ArgumentException("Specified brand does not exist!");
                    }

                }
                throw new DataAccessException("Values could not be read.");
            }
        }
    }
}
