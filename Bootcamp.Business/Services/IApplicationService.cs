using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.DTOs.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bootcamp.Business.Services
{
    public interface IApplicationService
    {
        Task<ApplicationResponseDto> CreateAsync(ApplicationRequestDto applicationRequestDto);
        Task<ApplicationResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<ApplicationResponseDto>> GetAllAsync();
        Task<ApplicationResponseDto> UpdateStatusAsync(ApplicationStatusUpdateRequestDto updateRequestDto);
        Task DeleteAsync(int id);
        
        // New methods
        Task<IEnumerable<ApplicationResponseDto>> GetByApplicantIdAsync(int applicantId);
        Task<IEnumerable<ApplicationResponseDto>> GetByBootcampIdAsync(int bootcampId);
        Task<bool> HasAppliedAsync(int applicantId, int bootcampId);
    }
} 