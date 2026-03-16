using HRSystem.Application.Interfaces;
using HRSystem.Domain.Entities;
using HRSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRDbContext _context;

        public EmployeeRepository(HRDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Employees
                .Include(e => e.Vacations)
                .OrderBy(e => e.EmployeeId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Vacations)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<bool> CodeExistsAsync(string code)
        {
            return await _context.Employees
                .AnyAsync(e => e.EmployeeCode == code);
        }

        public async Task<bool> NameExistsAsync(string name, int? excludeId = null)
        {
            return await _context.Employees
                .AnyAsync(e => e.FullName == name && e.EmployeeId != excludeId);
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            return Task.CompletedTask;
        }
    }
}