using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => GenderToString(src.Gender)))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => StatusToString(src.Status)))
            .ForMember(dest => dest.Birthday,
                opt => opt.MapFrom(src => src.Birthday.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dest => dest.CreateTime,
                opt => opt.MapFrom(src => src.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dest => dest.ModifyTime,
                opt => opt.MapFrom(src => src.ModifyTime.ToString("yyyy-MM-dd HH:mm:ss")));
    }

    private static string GenderToString(int gender) => gender switch
    {
        0 => "未知",
        1 => "男",
        2 => "女",
        _ => "未知"
    };

    private static string StatusToString(int status) => status switch
    {
        0 => "禁用",
        1 => "启用",
        _ => "未知"
    };
}