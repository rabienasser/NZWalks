using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionGetDTO>().ReverseMap();
            CreateMap<RegionPostDTO, Region>().ReverseMap();
            CreateMap<RegionPutDTO, Region>().ReverseMap();
            CreateMap<WalkPostDTO, Walk>().ReverseMap();
            CreateMap<Walk, WalkGetDTO>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<WalkPutDTO, Walk>().ReverseMap();
        }
    }
}
