using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.DTOs.Responses;
using System.Threading.Tasks;

namespace Bootcamp.Business.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ApplicantResponseDto> RegisterApplicantAsync(ApplicantRequestDto applicantRequestDto);
        Task<InstructorResponseDto> RegisterInstructorAsync(InstructorRequestDto instructorRequestDto);
        Task<EmployeeResponseDto> RegisterEmployeeAsync(EmployeeRequestDto employeeRequestDto);
    }
} 