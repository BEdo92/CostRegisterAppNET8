using AutoMapper;
using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.DTOs;

namespace CostRegisterAppNET8.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegisterDto, AppUser>();
        CreateMap<Cost, CostDto>()
            .ForMember(dest => dest.CostCategory, opt => opt.MapFrom(src => src.CostCategory.CategoryName));
        CreateMap<CostDto, Cost>()
            .ForMember(dest => dest.CostCategory, opt => opt.MapFrom(src => new CostCategory { CategoryName = src.CostCategory }));
    }
}
