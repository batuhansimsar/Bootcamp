using System;
using System.Collections.Generic;

namespace Bootcamp.Entities
{
    public class BootcampEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstructorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BootcampState BootcampState { get; set; }
        
        // Navigation properties
        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        
        public BootcampEntity()
        {
            Applications = new HashSet<Application>();
        }
    }
} 