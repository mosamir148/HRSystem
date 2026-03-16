using HRSystem.Application.DTOs.Employee;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET api/employees?page=1&pageSize=5
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _employeeService.GetAllAsync(page, pageSize);
            var count = await _employeeService.GetTotalCountAsync();

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { message = result.Error });

            return Ok(new
            {
                items = result.Data,
                totalCount = count.Data,
                totalPages = (int)Math.Ceiling(count.Data / (double)pageSize),
                currentPage = page,
                pageSize
            });
        }

        // GET api/employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _employeeService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { message = result.Error });

            return Ok(result.Data);
        }

        // POST api/employees
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDto dto)
        {
            var result = await _employeeService.CreateAsync(dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { message = result.Error });

            return StatusCode(201, result.Data);
        }

        // PUT api/employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeUpdateDto dto)
        {
            var result = await _employeeService.UpdateAsync(id, dto);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { message = result.Error });

            return Ok(result.Data);
        }

        // DELETE api/employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { message = result.Error });

            return NoContent();
        }
    }
}