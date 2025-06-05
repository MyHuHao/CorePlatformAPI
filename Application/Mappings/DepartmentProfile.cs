using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Application.Mappings;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<Department, DepartmentDto>()
            .ForMember(dest => dest.DeptLevel,
                opt => opt.MapFrom(src => src.DeptLevel.ToString()))
            .ForMember(dest => dest.CancelDate,
                opt => opt.MapFrom(src => src.CancelDate == null ? "" : src.CancelDate.Value.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.IsCancel,
                opt => opt.MapFrom(src => src.IsCancel.ToString()))
            .ForMember(dest => dest.CreatedTime,
                opt => opt.MapFrom(src => src.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dest => dest.ModifiedTime,
                opt => opt.MapFrom(src => src.ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss")));
    }
}