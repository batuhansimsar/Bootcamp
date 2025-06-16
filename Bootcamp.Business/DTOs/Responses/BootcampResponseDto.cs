using Bootcamp.Entities;
using System;

namespace Bootcamp.Business.DTOs.Responses
{
    public class BootcampResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BootcampState BootcampState { get; set; }
    }
} 