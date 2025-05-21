using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Enums;

namespace Application.Mappings;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.Birthday,
                opt =>
                    opt.MapFrom(src =>
                        src.Birthday.HasValue ? src.Birthday.Value.ToString("yyyy-MM-dd") : ""))
            .ForMember(dest => dest.CreatedTime,
                opt => opt.MapFrom(src => src.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dest => dest.ModifiedTime,
                opt => opt.MapFrom(src => src.ModifiedTime.ToString("yyyy-MM-dd HH:mm:ss")));
    }
}