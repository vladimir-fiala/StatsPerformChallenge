namespace StatsPerformChallengeAPI.Models.StatsPerformDTOv1
{
    public class MatchDTO
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public int GameWeek { get; set; }
        public TeamDTO HomeTeam { get; set; }
        public TeamDTO AwayTeam { get; set; }
        public string Venue { get; set; }
    }
}
