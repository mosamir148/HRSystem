using HRSystem.Application.Interfaces;
using HRSystem.Domain.Entities;
using HRSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Infrastructure.Repositories
{
    public class VacationRepository : IVacationRepository
    {
        private readonly HRDbContext _context;

        public VacationRepository(HRDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vacation>> GetByEmployeeAsync(int employeeId)
        {
            return await _context.Vacations
                .Where(v => v.EmployeeId == employeeId)
                .OrderBy(v => v.StartDate)
                .ToListAsync();
        }

        public async Task<Vacation?> GetByIdAsync(int vacationId)
        {
            return await _context.Vacations
                .FirstOrDefaultAsync(v => v.VacationId == vacationId);
        }

        public async Task<bool> HasOverlapAsync(int employeeId, DateOnly start, DateOnly end)
        {
            return await _context.Vacations
                .AnyAsync(v => v.EmployeeId == employeeId
                            && v.StartDate <= end
                            && v.EndDate >= start);
        }

        public async Task<int> GetUsedDaysByTypeAsync(int employeeId, string type, int year)
        {
            return await _context.Vacations
                .Where(v => v.EmployeeId == employeeId
                         && v.VacationType == type
                         && v.StartDate.Year == year)
                .SumAsync(v => v.DurationDays);
        }

        public async Task AddAsync(Vacation vacation)
        {
            await _context.Vacations.AddAsync(vacation);
        }

        public Task DeleteAsync(Vacation vacation)
        {
            _context.Vacations.Remove(vacation);
            return Task.CompletedTask;
        }
    }
}