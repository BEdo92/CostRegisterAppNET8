using AutoMapper;
using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.DTOs;

namespace CostRegisterAppNET8.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegisterDto, AppUser>();
    }
}
