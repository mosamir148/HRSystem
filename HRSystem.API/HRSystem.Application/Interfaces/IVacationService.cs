using HRSystem.Application.Common;
using HRSystem.Application.DTOs.Vacation;

namespace HRSystem.Application.Interfaces
{
    public interface IVacationService
    {
        Task<Result<IEnumerable<VacationResponseDto>>> GetByEmployeeAsync(int employeeId);
        Task<Result<VacationResponseDto>> CreateAsync(int employeeId, VacationCreateDto dto);
        Task<Result<bool>> DeleteAsync(int employeeId, int vacationId);
    }
}