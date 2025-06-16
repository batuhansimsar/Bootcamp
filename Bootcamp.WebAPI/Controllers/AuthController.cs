using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bootcamp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponse = await _authService.LoginAsync(loginRequestDto);
            return Ok(loginResponse);
        }

        [HttpPost("register/applicant")]
        public async Task<IActionResult> RegisterApplicant([FromBody] ApplicantRequestDto applicantRequestDto)
        {
            var registeredApplicant = await _authService.RegisterApplicantAsync(applicantRequestDto);
            return Ok(registeredApplicant);
        }

        [HttpPost("register/instructor")]
        public async Task<IActionResult> RegisterInstructor([FromBody] InstructorRequestDto instructorRequestDto)
        {
            var registeredInstructor = await _authService.RegisterInstructorAsync(instructorRequestDto);
            return Ok(registeredInstructor);
        }

        [HttpPost("register/employee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] EmployeeRequestDto employeeRequestDto)
        {
            var registeredEmployee = await _authService.RegisterEmployeeAsync(employeeRequestDto);
            return Ok(registeredEmployee);
        }
    }
} 