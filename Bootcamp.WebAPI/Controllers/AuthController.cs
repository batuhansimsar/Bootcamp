using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
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
        [EnableRateLimiting("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await _authService.LoginAsync(loginRequestDto);
            if (result == null)
                return Unauthorized(new { message = "Invalid email or password" });
                
            return Ok(result);
        }

        [HttpPost("register/applicant")]
        public async Task<IActionResult> RegisterApplicant([FromForm] ApplicantRequestDto applicantRequestDto)
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