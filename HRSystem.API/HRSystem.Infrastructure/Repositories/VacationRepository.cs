using HRSystem.Application.Interfaces;
using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Repositories
{
    public class VacationRepository : IVacationRepository
    {



        public Task AddAsync(Vacation vacation)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Vacation vacation)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vacation>> GetByEmployeeAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<Vacation?> GetByIdAsync(int vacationId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUsedDaysByTypeAsync(int employeeId, string type, int year)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasOverlapAsync(int employeeId, DateOnly start, DateOnly end)
        {
            throw new NotImplementedException();
        }
    }
}
