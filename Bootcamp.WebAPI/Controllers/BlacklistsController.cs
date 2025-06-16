using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bootcamp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlacklistsController : ControllerBase
    {
        private readonly IBlacklistService _blacklistService;

        public BlacklistsController(IBlacklistService blacklistService)
        {
            _blacklistService = blacklistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blacklists = await _blacklistService.GetAllAsync();
            return Ok(blacklists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var blacklist = await _blacklistService.GetByIdAsync(id);
            return Ok(blacklist);
        }

        [HttpGet("check/{applicantId}")]
        public async Task<IActionResult> IsApplicantBlacklisted(int applicantId)
        {
            var isBlacklisted = await _blacklistService.IsApplicantBlacklistedAsync(applicantId);
            return Ok(isBlacklisted);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlacklistRequestDto blacklistRequestDto)
        {
            var createdBlacklist = await _blacklistService.CreateAsync(blacklistRequestDto);
            return CreatedAtAction(nameof(GetById), new { id = createdBlacklist.Id }, createdBlacklist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BlacklistRequestDto blacklistRequestDto)
        {
            var updatedBlacklist = await _blacklistService.UpdateAsync(id, blacklistRequestDto);
            return Ok(updatedBlacklist);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blacklistService.DeleteAsync(id);
            return NoContent();
        }
    }
} 