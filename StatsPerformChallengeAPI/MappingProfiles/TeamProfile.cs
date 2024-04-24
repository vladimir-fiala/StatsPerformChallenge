using AutoMapper;
using StatsPerformChallengeAPI.Models.StatsPerformDTOv1;
using StatsPerformChallengeAPI.Models.TechChallenge;

namespace StatsPerformChallengeAPI.MappingProfiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<TeamDTO, Team>();
            CreateMap<Team, TeamDTO>();
        }
    }
}
