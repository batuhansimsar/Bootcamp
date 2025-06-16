using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.DTOs.Responses;
using Bootcamp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bootcamp.Business.Services
{
    public interface IBootcampService
    {
        Task<BootcampResponseDto> CreateAsync(BootcampRequestDto bootcampRequestDto);
        Task<BootcampResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<BootcampResponseDto>> GetAllAsync();
        Task<BootcampResponseDto> UpdateAsync(int id, BootcampRequestDto bootcampRequestDto);
        Task DeleteAsync(int id);
        Task<BootcampResponseDto> UpdateStateAsync(int id, BootcampState newState);
    }
} 