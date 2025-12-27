using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.Services;
using Bootcamp.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bootcamp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BootcampsController : ControllerBase
    {
        private readonly IBootcampService _bootcampService;

        public BootcampsController(IBootcampService bootcampService)
        {
            _bootcampService = bootcampService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagedRequestDto? pagedRequest)
        {
            // If pagination parameters provided, return paginated result
            if (pagedRequest != null && (pagedRequest.PageNumber > 0 || pagedRequest.PageSize > 0))
            {
                var pagedBootcamps = await _bootcampService.GetAllPagedAsync(pagedRequest);
                return Ok(pagedBootcamps);
            }
            
            // Otherwise return all (backward compatibility)
            var bootcamps = await _bootcampService.GetAllAsync();
            return Ok(bootcamps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bootcamp = await _bootcampService.GetByIdAsync(id);
            return Ok(bootcamp);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BootcampRequestDto bootcampRequestDto)
        {
            var createdBootcamp = await _bootcampService.CreateAsync(bootcampRequestDto);
            return CreatedAtAction(nameof(GetById), new { id = createdBootcamp.Id }, createdBootcamp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BootcampRequestDto bootcampRequestDto)
        {
            var updatedBootcamp = await _bootcampService.UpdateAsync(id, bootcampRequestDto);
            return Ok(updatedBootcamp);
        }

        [HttpPatch("{id}/state")]
        public async Task<IActionResult> UpdateState(int id, [FromBody] BootcampState newState)
        {
            var updatedBootcamp = await _bootcampService.UpdateStateAsync(id, newState);
            return Ok(updatedBootcamp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bootcampService.DeleteAsync(id);
            return NoContent();
        }
    }
} 