using AutoMapper;
using API.Data;
using API.DTOs;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // Registration
        CreateMap<RegisterDto, AppUser>();

        // Cost
        CreateMap<Cost, CostEntryDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CostCategory.CategoryName));
        CreateMap<CostEntryDto, Cost>();

        // Income
        CreateMap<Income, CostEntryDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.IncomeCategory.CategoryName));
        CreateMap<CostEntryDto, Income>();

        // Costplan
        CreateMap<CostPlan, CostEntryDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CostCategory.CategoryName));
        CreateMap<CostEntryDto, CostPlan>();
    }
}
