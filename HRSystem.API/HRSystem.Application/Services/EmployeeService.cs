using AutoMapper;
using HRSystem.Application.Common;
using HRSystem.Application.DTOs.Employee;
using HRSystem.Application.Interfaces;
using HRSystem.Domain.Entities;

namespace HRSystem.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<EmployeeResponseDto>>> GetAllAsync(int page, int pageSize)
        {
            var employees = await _unitOfWork.Employees.GetAllAsync(page, pageSize);
            return Result<IEnumerable<EmployeeResponseDto>>.Success(
                _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees));
        }
        public async Task<Result<int>> GetTotalCountAsync()
        {
            var count = await _unitOfWork.Employees.GetTotalCountAsync();
            return Result<int>.Success(count);
        }
        public async Task<Result<EmployeeResponseDto?>> GetByIdAsync(int id)
        {
            var emp = await _unitOfWork.Employees.GetByIdAsync(id);

            if (emp == null)
                return Result<EmployeeResponseDto?>.NotFound("الموظف غير موجود");

            return Result<EmployeeResponseDto?>.Success(_mapper.Map<EmployeeResponseDto>(emp));
        }

        public async Task<Result<EmployeeResponseDto>> CreateAsync(EmployeeCreateDto dto)
        {
            if (await _unitOfWork.Employees.CodeExistsAsync(dto.EmployeeCode))
                return Result<EmployeeResponseDto>.Failure($"رقم الموظف '{dto.EmployeeCode}' مستخدم من قبل");

            if (await _unitOfWork.Employees.NameExistsAsync(dto.FullName))
                return Result<EmployeeResponseDto>.Failure($"الاسم '{dto.FullName}' مستخدم من قبل");

            var emp = _mapper.Map<Employee>(dto);
            await _unitOfWork.Employees.AddAsync(emp);
            await _unitOfWork.SaveChangesAsync();
            return Result<EmployeeResponseDto>.Success(_mapper.Map<EmployeeResponseDto>(emp), 201);
        }

        public async Task<Result<EmployeeResponseDto>> UpdateAsync(int id, EmployeeUpdateDto dto)
        {
            var emp = await _unitOfWork.Employees.GetByIdAsync(id);

            if (emp == null)
                return Result<EmployeeResponseDto>.NotFound("الموظف غير موجود");

            if (await _unitOfWork.Employees.NameExistsAsync(dto.FullName, id))
                return Result<EmployeeResponseDto>.Failure($"الاسم '{dto.FullName}' مستخدم من قبل");

            _mapper.Map(dto, emp);
            await _unitOfWork.Employees.UpdateAsync(emp);
            await _unitOfWork.SaveChangesAsync();
            return Result<EmployeeResponseDto>.Success(_mapper.Map<EmployeeResponseDto>(emp));
        }



        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var emp = await _unitOfWork.Employees.GetByIdAsync(id);

            if (emp == null)
                return Result<bool>.NotFound("الموظف غير موجود");

            await _unitOfWork.Employees.DeleteAsync(emp);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}