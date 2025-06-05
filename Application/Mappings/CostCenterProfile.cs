using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Application.Mappings;

public class CostCenterProfile : Profile
{
    public CostCenterProfile()
    {
        CreateMap<CostCenter, CostCenterDto>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.CreatedTime,
                opt => opt.MapFrom(src => src.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dest => dest.ModifiedTime,
                opt => opt.MapFrom(src => src.ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss")));
    }
}