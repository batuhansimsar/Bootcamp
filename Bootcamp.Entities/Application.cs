using System;

namespace Bootcamp.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int BootcampId { get; set; }
        public ApplicationState ApplicationState { get; set; }
        
        // Navigation properties
        public virtual Applicant Applicant { get; set; }
        public virtual BootcampEntity Bootcamp { get; set; }
    }
} 