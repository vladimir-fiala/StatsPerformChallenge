using System.Text.Json.Serialization;

namespace StatsPerformChallengeAPI.Models.TechChallenge
{
    public class Branding
    {
        // We never want to display this in the response body, so let's ignore this property.
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string TeamId { get; set; }
        public string Name { get; set; }
        public string PrimaryColor { get; set; }
        // Do not display in the response if the value is null
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Abbreviation { get; set; }
    }
}
