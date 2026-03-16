using AutoMapper;
using HRSystem.Application.DTOs.Employee;
using HRSystem.Application.DTOs.Vacation;
using HRSystem.Domain.Entities;

namespace HRSystem.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeResponseDto>()
                .ForMember(dest => dest.TotalVacationDays,
                           opt => opt.MapFrom(src => src.Vacations.Sum(v => v.DurationDays)));

            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();

            // Vacation
            CreateMap<Vacation, VacationResponseDto>();
            CreateMap<VacationCreateDto, Vacation>();
        }
    }
}
