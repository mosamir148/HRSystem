using AutoMapper;
using HRSystem.Application.Common;
using HRSystem.Application.DTOs.Vacation;
using HRSystem.Application.Interfaces;
using HRSystem.Domain.Entities;

namespace HRSystem.Application.Services
{
    public class VacationService : IVacationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VacationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ── Get By Employee ──────────────────────────────────
        public async Task<Result<IEnumerable<VacationResponseDto>>> GetByEmployeeAsync(int employeeId)
        {
            var emp = await _unitOfWork.Employees.GetByIdAsync(employeeId);
            if (emp == null)
                return Result<IEnumerable<VacationResponseDto>>.NotFound("الموظف غير موجود");

            var vacations = await _unitOfWork.Vacations.GetByEmployeeAsync(employeeId);
            return Result<IEnumerable<VacationResponseDto>>.Success(
                _mapper.Map<IEnumerable<VacationResponseDto>>(vacations));
        }

        public async Task<Result<VacationResponseDto>> CreateAsync(int employeeId, VacationCreateDto dto)
        {
            var emp = await _unitOfWork.Employees.GetByIdAsync(employeeId);
            if (emp == null)
                return Result<VacationResponseDto>.NotFound("الموظف غير موجود");

            if (dto.DurationDays < 1 || dto.DurationDays > 30)
                return Result<VacationResponseDto>.Failure("المدة لا تقل عن يوم ولا تزيد عن 30 يوماً");

            // تحويل string لـ DateOnly
            var startDate = DateOnly.Parse(dto.StartDate);
            var endDate = startDate.AddDays(dto.DurationDays - 1);

            var hasOverlap = await _unitOfWork.Vacations.HasOverlapAsync(employeeId, startDate, endDate);
            if (hasOverlap)
                return Result<VacationResponseDto>.Failure("تداخل مع إجازة موجودة في نفس الفترة");

            var usedDays = await _unitOfWork.Vacations.GetUsedDaysByTypeAsync(
                employeeId, dto.VacationType, startDate.Year);

            if (usedDays + dto.DurationDays > 30)
                return Result<VacationResponseDto>.Failure(
                    $"تجاوز الحد السنوي لإجازة '{dto.VacationType}' — المتبقي {30 - usedDays} يوم فقط");

            var vacation = new Vacation
            {
                EmployeeId = employeeId,
                VacationType = dto.VacationType,
                StartDate = startDate,
                EndDate = endDate,
                DurationDays = dto.DurationDays
            };

            await _unitOfWork.Vacations.AddAsync(vacation);
            await _unitOfWork.SaveChangesAsync();

            return Result<VacationResponseDto>.Success(
                _mapper.Map<VacationResponseDto>(vacation), 201);
        }

        // ── Delete ───────────────────────────────────────────
        public async Task<Result<bool>> DeleteAsync(int employeeId, int vacationId)
        {
            var vacation = await _unitOfWork.Vacations.GetByIdAsync(vacationId);

            if (vacation == null || vacation.EmployeeId != employeeId)
                return Result<bool>.NotFound("الإجازة غير موجودة");

            await _unitOfWork.Vacations.DeleteAsync(vacation);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}