using System.Diagnostics;

namespace StatsPerformChallengeAPI.Models.TechChallenge
{
    public class Match
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public int GameWeek { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public string Venue { get; set; }

        public static async Task<IEnumerable<Match>> CustomizeMatchesByBranding(IEnumerable<Match> matches, IEnumerable<Branding> branding)
        {
            // Presented are 3 possible solutions (1 - naive; 2 - dictionary for faster lookup; 3 - asynchronous), times are listed by the stopwatch.Elapsed)
            // I assume that the very limited number of matches in testing data is making additional headroom needed for solution 3 too slow
            // Drawback of solution 2 is that the testing data contains MANY duplicates (which causes an exception when transforming IEnumerable to Dictionary, (unique key violation)
            // Culling the brandings through DistinctBy could be a violation of the test data - good point for discussion?

            /* ********* SOLUTION 1 **********  
            Stopwatch stopwatch1 = Stopwatch.StartNew();
            foreach (var match in matches)
            {
                var homeTeamBrand = branding.FirstOrDefault(brand => brand.TeamId == match.HomeTeam.Id);
                if (homeTeamBrand != null)
                {
                    match.HomeTeam.Brand = new Branding
                    {
                        Name = homeTeamBrand.Name,
                        PrimaryColor = homeTeamBrand.PrimaryColor,
                        Abbreviation = homeTeamBrand.Abbreviation
                    };
                }
                var awayTeamBrand = branding.FirstOrDefault(brand => brand.TeamId == match.AwayTeam.Id);
                if (awayTeamBrand != null)
                {
                    match.AwayTeam.Brand = new Branding
                    {
                        Name = awayTeamBrand.Name,
                        PrimaryColor = awayTeamBrand.PrimaryColor,
                        Abbreviation = awayTeamBrand.Abbreviation
                    };
                }
            }
            stopwatch1.Stop();
            var stopwatch1Elapsed = stopwatch1.Elapsed; // 3470 ms; 5136 ms; 6524 ms; 8530ms
            */

            /* ********* SOLUTION 2 ********** */
            // Technically, one could argue the DistinctBy and ToDictionary calls should be *after* Stopwatch.StartNew()
            branding = branding.DistinctBy(branding => branding.TeamId);
            Dictionary<string, Branding> teamBrandings = branding.ToDictionary(brand => brand.TeamId);
            Stopwatch stopwatch2 = Stopwatch.StartNew();

            foreach (var match in matches)
            {
                if (teamBrandings.TryGetValue(match.HomeTeam.Id, out var foundHomeTeamBranding))
                {
                    match.HomeTeam.Brand = new Branding
                    {
                        Name = foundHomeTeamBranding.Name,
                        PrimaryColor = foundHomeTeamBranding.PrimaryColor,
                        Abbreviation = foundHomeTeamBranding.Abbreviation
                    };
                }
                if (teamBrandings.TryGetValue(match.AwayTeam.Id, out var foundAwayTeamBranding))
                {
                    match.AwayTeam.Brand = new Branding
                    {
                        Name = foundAwayTeamBranding.Name,
                        PrimaryColor = foundAwayTeamBranding.PrimaryColor,
                        Abbreviation = foundAwayTeamBranding.Abbreviation
                    };
                }
            }
            stopwatch2.Stop();
            var stopwatch2Elapsed = stopwatch2.Elapsed; // 105 ms; 124ms; 121ms; 106ms
            /* ********* SOLUTION 3 ********** 
            

            Stopwatch stopwatch3 = Stopwatch.StartNew();
            var homeTeamsTask = Task.Run(() => { BrandHomeTeams(ref matches, teamBrandings); });
            var awayTeamsTask = Task.Run(() => { BrandAwayTeams(ref matches, teamBrandings); });
            await Task.WhenAll(homeTeamsTask, awayTeamsTask);
            stopwatch3.Stop();
            var stopwatch3Elapsed = stopwatch3.Elapsed; // 3840 ms; 990 ms; 1331ms; 4348ms
            */
            return matches;
        }

        private static void BrandHomeTeams(ref IEnumerable<Match> matches, Dictionary<string, Branding> teamBrandings)
        {
            foreach (var match in matches)
            {
                if (teamBrandings.TryGetValue(match.HomeTeam.Id, out var foundHomeTeamBranding))
                {
                    match.HomeTeam.Brand = new Branding
                    {
                        Name = foundHomeTeamBranding.Name,
                        PrimaryColor = foundHomeTeamBranding.PrimaryColor,
                        Abbreviation = foundHomeTeamBranding.Abbreviation
                    };
                }
            }
        }
        private static void BrandAwayTeams(ref IEnumerable<Match> matches, Dictionary<string, Branding> teamBrandings)
        {
            foreach (var match in matches)
            {
                if (teamBrandings.TryGetValue(match.AwayTeam.Id, out var foundAwayTeamBranding))
                {
                    match.AwayTeam.Brand = new Branding
                    {
                        Name = foundAwayTeamBranding.Name,
                        PrimaryColor = foundAwayTeamBranding.PrimaryColor,
                        Abbreviation = foundAwayTeamBranding.Abbreviation
                    };
                }
            }
        }
    }
}
