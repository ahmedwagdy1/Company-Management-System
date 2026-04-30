using Application.BLL.DTOS.EmployeeDTOS;
using Application.DAL.Data.Models.EmployeeModul;
using AutoMapper;

namespace Application.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Gender, option => option.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, option => option.MapFrom(src => src.EmployeeTypes))
                .ForMember(dest => dest.Department, option => option.MapFrom(src => src.Department != null ? src.Department.Name : null));

            CreateMap<Employee, EmployeeDetalisDto>()
                .ForMember(dest => dest.Gender, option => option.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, option => option.MapFrom(src => src.EmployeeTypes))
                .ForMember(dest => dest.HiringDate, option => option.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(dest => dest.Department, option => option.MapFrom(src => src.Department != null ? src.Department.Name : null));

            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, option => option.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdatedEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, option => option.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));
            
        }
    }
}
