using AutoMapper;
using StatsPerformChallengeAPI.Models.StatsPerformDTOv1;
using StatsPerformChallengeAPI.Models.TechChallenge;

namespace StatsPerformChallengeAPI.MappingProfiles
{
    public class BrandingDefinitionProfile : Profile
    {
        public BrandingDefinitionProfile()
        {
            CreateMap<BrandingDefinitionDTO, BrandingDefinition>();
            CreateMap<BrandingDefinition, BrandingDefinitionDTO>();
        }
    }
}

