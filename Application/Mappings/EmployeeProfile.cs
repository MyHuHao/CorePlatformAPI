using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Application.Mappings;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.UserType,
                opt => opt.MapFrom(src => src.UserType.ToString()))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.EmpEntryDate,
                opt => opt.MapFrom(src =>
                    src.EmpEntryDate.HasValue ? src.EmpEntryDate.Value.ToString("yyyy-MM-dd") : ""))
            .ForMember(dest => dest.EmpDepartureDate,
                opt => opt.MapFrom(src =>
                    src.EmpDepartureDate.HasValue ? src.EmpDepartureDate.Value.ToString("yyyy-MM-dd") : ""))
            .ForMember(dest => dest.DeliveredDate,
                opt => opt.MapFrom(src =>
                    src.DeliveredDate.HasValue ? src.DeliveredDate.Value.ToString("yyyy-MM-dd") : ""))
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