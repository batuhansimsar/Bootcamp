using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.DTOs.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bootcamp.Business.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeResponseDto> CreateAsync(EmployeeRequestDto employeeRequestDto);
        Task<EmployeeResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<EmployeeResponseDto>> GetAllAsync();
        Task<EmployeeResponseDto> UpdateAsync(int id, EmployeeRequestDto employeeRequestDto);
        Task DeleteAsync(int id);
    }
} 