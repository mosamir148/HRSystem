using HRSystem.Application.Interfaces;
using HRSystem.Application.Mapper;
using HRSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HRSystem.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            // Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            //  services.AddScoped<IVacationService, VacationService>();

            return services;
        }
    }
}