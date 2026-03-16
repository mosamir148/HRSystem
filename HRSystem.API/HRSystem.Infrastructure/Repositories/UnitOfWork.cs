using HRSystem.Application.Interfaces;
using HRSystem.Infrastructure.Data;

namespace HRSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HRDbContext _context;

        public IEmployeeRepository Employees { get; }
        public IVacationRepository Vacations { get; }

        public UnitOfWork(HRDbContext context,
                          IEmployeeRepository employees,
                          IVacationRepository vacations)
        {
            _context = context;
            Employees = employees;
            Vacations = vacations;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}