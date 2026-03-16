using HRSystem.Domain.Entities;

namespace HRSystem.Application.Interfaces
{
    public interface IVacationRepository
    {
        Task<IEnumerable<Vacation>> GetByEmployeeAsync(int employeeId);
        Task<bool> HasOverlapAsync(int employeeId, DateOnly start, DateOnly end);
        Task<int> GetUsedDaysByTypeAsync(int employeeId, string type, int year);
        Task AddAsync(Vacation vacation);
        Task<Vacation?> GetByIdAsync(int vacationId);
        Task DeleteAsync(Vacation vacation);

    }
}
