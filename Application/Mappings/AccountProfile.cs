using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Application.Mappings;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDto>()
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType.ToString()))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive.ToString()))
            .ForMember(dest => dest.LastLoginTime, opt => opt.MapFrom(src => src.LastLoginTime.HasValue ? src.LastLoginTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""))
            .ForMember(dest => dest.FailedLoginAttempts, opt => opt.MapFrom(src => src.FailedLoginAttempts.ToString()))
            .ForMember(dest => dest.IsLocked, opt => opt.MapFrom(src => src.IsLocked.ToString()));
    }
}