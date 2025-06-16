using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bootcamp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantsController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applicants = await _applicantService.GetAllAsync();
            return Ok(applicants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var applicant = await _applicantService.GetByIdAsync(id);
            return Ok(applicant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ApplicantRequestDto applicantRequestDto)
        {
            var createdApplicant = await _applicantService.CreateAsync(applicantRequestDto);
            return CreatedAtAction(nameof(GetById), new { id = createdApplicant.Id }, createdApplicant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ApplicantRequestDto applicantRequestDto)
        {
            var updatedApplicant = await _applicantService.UpdateAsync(id, applicantRequestDto);
            return Ok(updatedApplicant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _applicantService.DeleteAsync(id);
            return NoContent();
        }
    }
} 