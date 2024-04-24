using System.Text.Json.Serialization;

namespace StatsPerformChallengeAPI.Models.TechChallenge
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        // Do not display in the response if the value is null
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Branding Brand { get; set; }

    }
}
