namespace StatsPerformChallengeAPI.Models.TechChallenge
{
    public class League
    {
        // If we had more info about the possible values, we could for example add [StringLength(64, MinimumLength = 4)] or [Required]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
