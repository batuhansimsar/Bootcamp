using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.DTOs.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bootcamp.Business.Services
{
    public interface IBlacklistService
    {
        Task<BlacklistResponseDto> CreateAsync(BlacklistRequestDto blacklistRequestDto);
        Task<BlacklistResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<BlacklistResponseDto>> GetAllAsync();
        Task<BlacklistResponseDto> UpdateAsync(int id, BlacklistRequestDto blacklistRequestDto);
        Task DeleteAsync(int id);
        Task<bool> IsApplicantBlacklistedAsync(int applicantId);
    }
} 