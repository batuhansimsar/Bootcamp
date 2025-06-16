using System;

namespace Bootcamp.Business.DTOs.Responses
{
    public class BlacklistResponseDto
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public int ApplicantId { get; set; }
        public string ApplicantName { get; set; }
    }
} 