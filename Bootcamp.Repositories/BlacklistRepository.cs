using Bootcamp.Core.Repositories;
using Bootcamp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bootcamp.Repositories
{
    public class BlacklistRepository : Repository<Blacklist>, IBlacklistRepository
    {
        public BlacklistRepository(BootcampDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByApplicantIdAsync(int applicantId)
        {
            return await _dbSet.AnyAsync(b => b.ApplicantId == applicantId);
        }
    }
} 