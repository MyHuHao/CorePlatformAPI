using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Application.Mappings;

public class RoleGroupProfile : Profile
{
    public RoleGroupProfile()
    {
        CreateMap<RoleGroup, RoleGroupDto>()
            .ForMember(dest => dest.CreatedTime,
                opt => opt.MapFrom(src => src.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dest => dest.ModifiedTime,
                opt => opt.MapFrom(src => src.ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss")));
    }
}