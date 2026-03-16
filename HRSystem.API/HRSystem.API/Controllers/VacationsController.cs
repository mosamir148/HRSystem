using HRSystem.Application.DTOs.Vacation;
using HRSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.API.Controllers
{
    [ApiController]
    [Route("api/employees/{employeeId}/vacations")]
    public class VacationsController : ControllerBase
    {
        private readonly IVacationService _vacationService;

        public VacationsController(IVacationService vacationService)
        {
            _vacationService = vacationService;
        }

        // GET api/employees/5/vacations
        [HttpGet]
        public async Task<IActionResult> GetAll(int employeeId)
        {
            var result = await _vacationService.GetByEmployeeAsync(employeeId);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { message = result.Error });

            return Ok(result.Data);
        }

        // POST api/employees/5/vacations
        [HttpPost]
        public async Task<IActionResult> Create(int employeeId, [FromBody] VacationCreateDto dto)
        {
            var result = await _vacationService.CreateAsync(employeeId, dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { message = result.Error });

            return StatusCode(201, result.Data);
        }

        // DELETE api/employees/5/vacations/10
        [HttpDelete("{vacationId}")]
        public async Task<IActionResult> Delete(int employeeId, int vacationId)
        {
            var result = await _vacationService.DeleteAsync(employeeId, vacationId);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { message = result.Error });

            return NoContent();
        }
    }
}