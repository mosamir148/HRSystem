using HRSystem.Application.Common;
using HRSystem.Application.DTOs.Employee;

public interface IEmployeeService
{
    Task<Result<IEnumerable<EmployeeResponseDto>>> GetAllAsync(int page, int pageSize);
    Task<Result<EmployeeResponseDto?>> GetByIdAsync(int id);
    Task<Result<EmployeeResponseDto>> CreateAsync(EmployeeCreateDto dto);
    Task<Result<EmployeeResponseDto>> UpdateAsync(int id, EmployeeUpdateDto dto);
    Task<Result<bool>> DeleteAsync(int id);
    Task<Result<int>> GetTotalCountAsync();
}