using Microsoft.AspNetCore.Http;
namespace Bootcamp.Business.DTOs.Requests
{
    public class ApplicantRequestDto : UserRequestDto
    {
        public string About { get; set; }
        public IFormFile? ResumeFile { get; set; }
    }
} 