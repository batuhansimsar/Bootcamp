using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bootcamp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applications = await _applicationService.GetAllAsync();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var application = await _applicationService.GetByIdAsync(id);
            return Ok(application);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ApplicationRequestDto applicationRequestDto)
        {
            var createdApplication = await _applicationService.CreateAsync(applicationRequestDto);
            return CreatedAtAction(nameof(GetById), new { id = createdApplication.Id }, createdApplication);
        }

        [HttpPatch("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] ApplicationStatusUpdateRequestDto updateRequestDto)
        {
            var updatedApplication = await _applicationService.UpdateStatusAsync(updateRequestDto);
            return Ok(updatedApplication);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _applicationService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("my-applications/{applicantId}")]
        public async Task<IActionResult> GetMyApplications(int applicantId)
        {
            var applications = await _applicationService.GetByApplicantIdAsync(applicantId);
            return Ok(applications);
        }

        [HttpGet("bootcamp/{bootcampId}")]
        public async Task<IActionResult> GetByBootcampId(int bootcampId)
        {
            var applications = await _applicationService.GetByBootcampIdAsync(bootcampId);
            return Ok(applications);
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckIfApplied([FromQuery] int applicantId, [FromQuery] int bootcampId)
        {
            var hasApplied = await _applicationService.HasAppliedAsync(applicantId, bootcampId);
            return Ok(new { hasApplied });
        }
    }
}