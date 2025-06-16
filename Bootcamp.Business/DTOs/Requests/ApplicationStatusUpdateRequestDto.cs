using Bootcamp.Entities;

namespace Bootcamp.Business.DTOs.Requests
{
    public class ApplicationStatusUpdateRequestDto
    {
        public int ApplicationId { get; set; }
        public ApplicationState NewState { get; set; }
    }
} 