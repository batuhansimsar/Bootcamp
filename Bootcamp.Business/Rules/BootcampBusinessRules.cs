using Bootcamp.Core.Repositories;
using Bootcamp.Entities;
using System;
using System.Threading.Tasks;

namespace Bootcamp.Business.Rules
{
    public class BootcampBusinessRules
    {
        private readonly IBootcampRepository _bootcampRepository;
        private readonly IInstructorRepository _instructorRepository;

        public BootcampBusinessRules(IBootcampRepository bootcampRepository, IInstructorRepository instructorRepository)
        {
            _bootcampRepository = bootcampRepository;
            _instructorRepository = instructorRepository;
        }

        public void CheckIfStartDateBeforeEndDate(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                throw new Exception("Start date must be before end date");
        }

        public async Task CheckIfBootcampNameExistsAsync(string name)
        {
            var exists = await _bootcampRepository.ExistsByNameAsync(name);
            if (exists)
                throw new Exception("A bootcamp with this name already exists");
        }

        public async Task CheckIfInstructorExistsAsync(int instructorId)
        {
            var instructor = await _instructorRepository.GetByIdAsync(instructorId);
            if (instructor == null)
                throw new Exception("Instructor not found");
        }

        public async Task CheckIfBootcampIsActiveAsync(int bootcampId)
        {
            var bootcamp = await _bootcampRepository.GetByIdAsync(bootcampId);
            if (bootcamp == null)
                throw new Exception("Bootcamp not found");
                
            if (bootcamp.BootcampState != BootcampState.OPEN_FOR_APPLICATION)
                throw new Exception("This bootcamp is not open for applications");
        }
    }
} 