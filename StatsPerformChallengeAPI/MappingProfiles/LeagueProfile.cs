using AutoMapper;
using StatsPerformChallengeAPI.Models.StatsPerformDTOv1;
using StatsPerformChallengeAPI.Models.TechChallenge;

namespace StatsPerformChallengeAPI.MappingProfiles
{
    public class LeagueProfile : Profile
    {
        public LeagueProfile()
        {
            CreateMap<LeagueDTO, League>();
            CreateMap<League, LeagueDTO>();
        }
    }
}
