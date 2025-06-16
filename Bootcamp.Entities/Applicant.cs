using System;
using System.Collections.Generic;

namespace Bootcamp.Entities
{
    public class Applicant : User
    {
        public string About { get; set; }
        
        // Navigation properties
        public virtual ICollection<Application> Applications { get; set; }
        
        public Applicant()
        {
            Applications = new HashSet<Application>();
        }
    }
} 