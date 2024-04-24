using AutoMapper;
using StatsPerformChallengeAPI.Models.StatsPerformDTOv1;
using StatsPerformChallengeAPI.Models.TechChallenge;

namespace StatsPerformChallengeAPI.MappingProfiles
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<MatchDTO, Match>();
            CreateMap<Match, MatchDTO>();
        }
    }
}
