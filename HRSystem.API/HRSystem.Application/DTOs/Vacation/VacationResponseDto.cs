using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOs.Vacation
{
    public class VacationResponseDto
    {
        public int VacationId { get; set; }
        public int EmployeeId { get; set; }
        public string VacationType { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int DurationDays { get; set; }
    }
}
