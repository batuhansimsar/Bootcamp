using Bootcamp.Core.Repositories;
using Bootcamp.Entities;

namespace Bootcamp.Repositories
{
    public class EmployeeRepository : UserRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BootcampDbContext context) : base(context)
        {
        }
    }
} 