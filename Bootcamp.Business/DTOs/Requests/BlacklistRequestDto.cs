using System;

namespace Bootcamp.Business.DTOs.Requests
{
    public class BlacklistRequestDto
    {
        public string Reason { get; set; }
        public int ApplicantId { get; set; }
    }
} 