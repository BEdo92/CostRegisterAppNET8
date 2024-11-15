﻿using AutoMapper;
using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.DTOs;

namespace CostRegisterAppNET8.Helpers;

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
    }
}
