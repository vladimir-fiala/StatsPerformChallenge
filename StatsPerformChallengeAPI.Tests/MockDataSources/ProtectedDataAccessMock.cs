using StatsPerformChallengeAPI.Models.Interfaces;
using StatsPerformChallengeAPI.Models.TechChallenge;

namespace StatsPerformChallengeAPI.Tests.MockDataSources
{
    internal class ProtectedDataAccessMock : IProtectedDataAccess
    {
        private readonly List<BrandingDefinition> brandDefinitions;
        private readonly Dictionary<string, List<Branding>> brandings;
        private readonly List<Branding> brandingNo1;
        private readonly List<Branding> brandingNo2;
        private readonly List<League> leagues;
        private readonly List<Match> unbrandedMatchesQuidditchLeague;
        public ProtectedDataAccessMock()
        {
            brandDefinitions =
            [
                new()
                {
                    Id = "brandingDef001",
                    Name = "Branding no. 1"
                },
                new()
                {
                    Id = "brandingDef002",
                    Name = "Branding no. 2"
                }
            ];

            brandingNo1 =
            [
                new()
                {
                    TeamId = "AppArr",
                    Name = "Arrows",
                    PrimaryColor = "#6dd0f5FF",
                    Abbreviation = "AA"
                },
                new()
                {
                    TeamId = "HolHar",
                    Name = "Harpies",
                    PrimaryColor = "#2a5b0fFF",
                    Abbreviation = "HH"
                },
                new()
                {
                    TeamId = "MonMag",
                    Name = "Magpies",
                    PrimaryColor = "#000000FF",
                    Abbreviation = "MM"
                }
            ];
            brandingNo2 =
            [
                new()
                {
                    TeamId = "AppArr",
                    Name = "Appleby Arrows (England)",
                    PrimaryColor = "#0000FFFF",
                    Abbreviation = "Apple"
                },
                new()
                {
                    TeamId = "HolHar",
                    Name = "Holyhead Harpies (Wales)",
                    PrimaryColor = "#00FF00",
                    Abbreviation = "Holyh"
                },
                new()
                {
                    TeamId = "MonMag",
                    Name = "Montrose Magpies (Scotland)",
                    PrimaryColor = "#FFFFFFFF",
                    Abbreviation = "Montr"
                }
            ];
            brandings = new Dictionary<string, List<Branding>>
            {
                { "brandingDef001", brandingNo1 },
                { "brandingDef002", brandingNo2 }
            };
            leagues =
            [
                new()
                {
                    Id = "quidditchLeague",
                    Name = "British-Irish League"
                }
            ];
            unbrandedMatchesQuidditchLeague =
            [
                new()
                {
                    Id = "1",
                    Date = DateTime.Parse("2022-10-29T14:00:00"),
                    GameWeek = 7,
                    HomeTeam = new()
                    {
                        Id = "AppArr",
                        Name = "Appleby Arrows"
                    },
                    AwayTeam = new()
                    {
                        Id = "HolHar",
                        Name = "Holyhead Harpies"
                    },
                    Venue = "Holyhead, Holy Island stadium"
                },
                new()
                {
                    Id = "2",
                    Date = DateTime.Parse("2022-10-28T12:00:00"),
                    GameWeek = 7,
                    HomeTeam = new()
                    {
                        Id = "MonMag",
                        Name = "Montrose Magpies"
                    },
                    AwayTeam = new()
                    {
                        Id = "AppArr",
                        Name = "Appleby Arrows"
                    },
                    Venue = "Montrose Quidditch stadium"
                }
            ];
        }

#pragma warning disable CS1998 
        public async Task<IEnumerable<BrandingDefinition>> GetBrandingsAsync()
        {
            return brandDefinitions;
        }

        public async Task<IEnumerable<Branding>> GetBrandingByIdAsync(string brandId)
        {
            if (brandings.TryGetValue(brandId, out var brandDefinition))
            {
                return brandDefinition;
            }
            else
                throw new ArgumentException("Specified branding does not exist!");
        }

        public async Task<IEnumerable<League>> GetLeaguesAsync()
        {
            return leagues;
        }

        public async Task<IEnumerable<Match>> GetMatchesAsync(string leagueId)
        {
            if (leagueId == "quidditchLeague")
                return unbrandedMatchesQuidditchLeague;
            else
                throw new ArgumentException("Specified league does not exist!");
        }

        public async Task<IEnumerable<Match>> GetMatchesAsync(string leagueId, string brandingId)
#pragma warning restore CS1998
        {
            if (leagueId != "quidditchLeague")
                throw new ArgumentException("Specified league does not exist!");

            if (brandings.TryGetValue(brandingId, out var brandDefinition))
            {
                return await Match.CustomizeMatchesByBranding(unbrandedMatchesQuidditchLeague, brandDefinition);
            }
            else
                throw new ArgumentException("Specified branding does not exist!");
        }
    }
}
