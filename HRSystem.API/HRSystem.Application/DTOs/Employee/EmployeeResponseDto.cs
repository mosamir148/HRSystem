using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOs.Employee
{
  
        public class EmployeeResponseDto
        {
            public int EmployeeId { get; set; }
            public string EmployeeCode { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public DateOnly? BirthDate { get; set; }
            public string? Qualification { get; set; }
            public int TotalVacationDays { get; set; }
        }
    
}
