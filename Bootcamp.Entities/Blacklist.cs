using System;

namespace Bootcamp.Entities
{
    public class Blacklist
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public int ApplicantId { get; set; }
        
        // Navigation property
        public virtual Applicant Applicant { get; set; }
    }
} 