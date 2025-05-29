using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Application.Mappings;

public class ApiLogProfile : Profile
{
    public ApiLogProfile()
    {
        CreateMap<ApiLog, ApiLogDto>()
            .ForMember(dest => dest.Duration,
                opt => opt.MapFrom(src => Math.Round(src.Duration / 1000.0, 3)))
            .ForMember(dest => dest.RequestTime,
                opt => opt.MapFrom(src => src.RequestTime.ToString("yyyy-MM-dd HH:mm:ss")));
    }
}