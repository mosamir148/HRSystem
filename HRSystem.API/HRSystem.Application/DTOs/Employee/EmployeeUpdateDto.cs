using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOs.Employee
{
        public class EmployeeUpdateDto
        {
            public string FullName { get; set; } = string.Empty;
            public DateOnly? BirthDate { get; set; }
            public string? Qualification { get; set; }
        }
    
}
