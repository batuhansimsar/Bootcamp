using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bootcamp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var instructors = await _instructorService.GetAllAsync();
            return Ok(instructors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            return Ok(instructor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InstructorRequestDto instructorRequestDto)
        {
            var createdInstructor = await _instructorService.CreateAsync(instructorRequestDto);
            return CreatedAtAction(nameof(GetById), new { id = createdInstructor.Id }, createdInstructor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InstructorRequestDto instructorRequestDto)
        {
            var updatedInstructor = await _instructorService.UpdateAsync(id, instructorRequestDto);
            return Ok(updatedInstructor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _instructorService.DeleteAsync(id);
            return NoContent();
        }
    }
} 