using Bootcamp.Entities;

namespace Bootcamp.Business.DTOs.Responses
{
    public class ApplicationResponseDto
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public int BootcampId { get; set; }
        public string BootcampName { get; set; }
        public ApplicationState ApplicationState { get; set; }
    }
} 