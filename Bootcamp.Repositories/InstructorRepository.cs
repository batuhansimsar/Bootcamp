using Bootcamp.Core.Repositories;
using Bootcamp.Entities;

namespace Bootcamp.Repositories
{
    public class InstructorRepository : UserRepository<Instructor>, IInstructorRepository
    {
        public InstructorRepository(BootcampDbContext context) : base(context)
        {
        }
    }
} 