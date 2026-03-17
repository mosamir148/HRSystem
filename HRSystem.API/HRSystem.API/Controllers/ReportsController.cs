using HRSystem.API.Reports;
using HRSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace HRSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IVacationService _vacationService;

        public ReportsController(IEmployeeService employeeService, IVacationService vacationService)
        {
            _employeeService = employeeService;
            _vacationService = vacationService;
        }

        // GET api/reports/employee/5
        [HttpGet("employee/{id}")]
        public async Task<IActionResult> GetEmployeeReport(int id)
        {
            var empResult = await _employeeService.GetByIdAsync(id);
            if (!empResult.IsSuccess)
                return NotFound(new { message = empResult.Error });

            var vacResult = await _vacationService.GetByEmployeeAsync(id);
            var vacations = vacResult.IsSuccess ? vacResult.Data! : Enumerable.Empty<HRSystem.Application.DTOs.Vacation.VacationResponseDto>();

            QuestPDF.Settings.License = LicenseType.Community;

            var report = new EmployeeReport(empResult.Data!, vacations);
            var pdf = report.GeneratePdf();

            return File(pdf, "application/pdf", $"Employee_{empResult.Data!.EmployeeCode}.pdf");
        }
    }
}