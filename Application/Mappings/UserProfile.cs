using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Enums;

namespace Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => src.Gender.ToGenderString()))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToStatusString()))
            .ForMember(dest => dest.Birthday,
                opt => opt.MapFrom(src => src.Birthday.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dest => dest.CreateTime,
                opt => opt.MapFrom(src => src.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dest => dest.ModifyTime,
                opt => opt.MapFrom(src => src.ModifyTime.ToString("yyyy-MM-dd HH:mm:ss")));
    }
}