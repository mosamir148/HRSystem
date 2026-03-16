namespace HRSystem.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
        public string? Qualification { get; set; }

        public ICollection<Vacation> Vacations { get; set; } = new List<Vacation>();
    }

}
