namespace HRSystem.Domain.Entities
{
    public class Vacation
    {
        public int VacationId { get; set; }
        public int EmployeeId { get; set; }
        public string VacationType { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int DurationDays { get; set; }

        public Employee? Employee { get; set; }
    }
}