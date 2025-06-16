using AutoMapper;
using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.DTOs.Responses;
using Bootcamp.Core.UnitOfWork;
using Bootcamp.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bootcamp.Business.Services
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EmployeeResponseDto> CreateAsync(EmployeeRequestDto employeeRequestDto)
        {
            // Check if email already exists
            var existingEmployee = await _unitOfWork.Employees.GetByEmailAsync(employeeRequestDto.Email);
            if (existingEmployee != null)
                throw new Exception("Email already in use");
            
            var employee = _mapper.Map<Employee>(employeeRequestDto);
            
            // Password hashing would be handled here in a real application
            employee.PasswordHash = new byte[0]; // Placeholder
            employee.PasswordSalt = new byte[0]; // Placeholder
            
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<EmployeeResponseDto>(employee);
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
                throw new Exception("Employee not found");
                
            _unitOfWork.Employees.Remove(employee);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<EmployeeResponseDto>> GetAllAsync()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);
        }

        public async Task<EmployeeResponseDto> GetByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
                throw new Exception("Employee not found");
                
            return _mapper.Map<EmployeeResponseDto>(employee);
        }

        public async Task<EmployeeResponseDto> UpdateAsync(int id, EmployeeRequestDto employeeRequestDto)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
                throw new Exception("Employee not found");
                
            // Check if email is being changed and if it already exists
            if (employee.Email != employeeRequestDto.Email)
            {
                var existingEmployee = await _unitOfWork.Employees.GetByEmailAsync(employeeRequestDto.Email);
                if (existingEmployee != null)
                    throw new Exception("Email already in use");
            }
                
            _mapper.Map(employeeRequestDto, employee);
            
            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<EmployeeResponseDto>(employee);
        }
    }
} 