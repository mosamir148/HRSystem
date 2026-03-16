using AutoMapper;
using HRSystem.Application.DTOs.Employee;
using HRSystem.Application.DTOs.Vacation;
using HRSystem.Domain.Entities;

namespace HRSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ── Employee ──────────────────────────────────────
            CreateMap<Employee, EmployeeResponseDto>()
                .ForMember(dest => dest.TotalVacationDays,
                           opt => opt.MapFrom(src => src.Vacations.Sum(v => v.DurationDays)));

            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();

            // ── Vacation ──────────────────────────────────────
            CreateMap<Vacation, VacationResponseDto>()
                .ForMember(dest => dest.StartDate,
                           opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.EndDate,
                           opt => opt.MapFrom(src => src.EndDate.ToString("yyyy-MM-dd")));

            CreateMap<VacationCreateDto, Vacation>()
       .ForMember(dest => dest.StartDate,
                  opt => opt.MapFrom(src => DateOnly.Parse(src.StartDate)))
       .ForMember(dest => dest.EndDate,
                  opt => opt.Ignore())
       .ForMember(dest => dest.DurationDays,
                  opt => opt.MapFrom(src => src.DurationDays));
        }
    }
}