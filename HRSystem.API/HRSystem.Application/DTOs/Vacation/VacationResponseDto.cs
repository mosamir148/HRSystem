namespace HRSystem.Application.DTOs.Vacation
{
    public class VacationResponseDto
    {
        public int VacationId { get; set; }
        public int EmployeeId { get; set; }
        public string VacationType { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public int DurationDays { get; set; }
    }
}
