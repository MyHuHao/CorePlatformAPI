using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Application.Mappings;

public class WebMenuProfile : Profile
{
    public WebMenuProfile()
    {
        CreateMap<WebMenu, WebMenuDto>()
            .ForMember(dest => dest.Sequence,
                opt => opt.MapFrom(src => src.Sequence.ToString()))
            .ForMember(dest => dest.IsFrame,
                opt => opt.MapFrom(src => src.IsFrame.ToString()))
            .ForMember(dest => dest.IsCache,
                opt => opt.MapFrom(src => src.IsCache.ToString()))
            .ForMember(dest => dest.IsVisible,
                opt => opt.MapFrom(src => src.IsVisible.ToString()))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));
    }
}