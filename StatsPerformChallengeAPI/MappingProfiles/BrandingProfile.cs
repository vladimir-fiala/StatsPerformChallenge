using AutoMapper;
using StatsPerformChallengeAPI.Models.StatsPerformDTOv1;
using StatsPerformChallengeAPI.Models.TechChallenge;

namespace StatsPerformChallengeAPI.MappingProfiles
{
    public class BrandingProfile : Profile
    {
        public BrandingProfile()
        {
            CreateMap<BrandingDetailsDTO, Branding>();
            CreateMap<Branding, BrandingDetailsDTO>();
        }
    }
}
