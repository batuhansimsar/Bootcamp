using System;

namespace Bootcamp.Business.DTOs.Requests
{
    public class BootcampRequestDto
    {
        public string Name { get; set; }
        public int InstructorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
} 