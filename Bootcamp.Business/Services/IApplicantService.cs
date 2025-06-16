using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.DTOs.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bootcamp.Business.Services
{
    public interface IApplicantService
    {
        Task<ApplicantResponseDto> CreateAsync(ApplicantRequestDto applicantRequestDto);
        Task<ApplicantResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<ApplicantResponseDto>> GetAllAsync();
        Task<ApplicantResponseDto> UpdateAsync(int id, ApplicantRequestDto applicantRequestDto);
        Task DeleteAsync(int id);
    }
} 