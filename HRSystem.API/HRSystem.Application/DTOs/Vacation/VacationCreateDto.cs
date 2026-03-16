namespace HRSystem.Application.DTOs.Vacation
{
    public class VacationCreateDto
    {
        public string VacationType { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public int DurationDays { get; set; }
    }
}
