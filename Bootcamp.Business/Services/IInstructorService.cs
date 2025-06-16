using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.DTOs.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bootcamp.Business.Services
{
    public interface IInstructorService
    {
        Task<InstructorResponseDto> CreateAsync(InstructorRequestDto instructorRequestDto);
        Task<InstructorResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<InstructorResponseDto>> GetAllAsync();
        Task<InstructorResponseDto> UpdateAsync(int id, InstructorRequestDto instructorRequestDto);
        Task DeleteAsync(int id);
    }
} 