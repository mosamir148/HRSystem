using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<bool> CodeExistsAsync(string code);
        Task<bool> NameExistsAsync(string name, int? excludeId = null);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
    }
}
