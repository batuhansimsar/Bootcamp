using Bootcamp.Core.Repositories;
using Bootcamp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bootcamp.Repositories
{
    public class BootcampRepository : Repository<BootcampEntity>, IBootcampRepository
    {
        public BootcampRepository(BootcampDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbSet.AnyAsync(b => b.Name == name);
        }

        public async Task<bool> IsActiveAsync(int bootcampId)
        {
            var bootcamp = await _dbSet.FindAsync(bootcampId);
            if (bootcamp == null)
                return false;
                
            return bootcamp.BootcampState == BootcampState.OPEN_FOR_APPLICATION;
        }
    }
} 