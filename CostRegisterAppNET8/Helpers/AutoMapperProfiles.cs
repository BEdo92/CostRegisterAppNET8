﻿using API.Data;
using API.DTOs;
using AutoMapper;

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
        CreateMap<CostPlan, CostPlanEntryDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CostCategory.CategoryName));
        CreateMap<CostPlanEntryDto, CostPlan>();

        // User edit
        CreateMap<MemberDto, AppUser>();
        CreateMap<AppUser, MemberDto>();
    }
}
