using Bootcamp.Core.Repositories;
using Bootcamp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bootcamp.Repositories
{
    public class ApplicantRepository : UserRepository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(BootcampDbContext context) : base(context)
        {
        }

        public async Task<bool> IsBlacklistedAsync(int applicantId)
        {
            return await _context.Blacklists.AnyAsync(b => b.ApplicantId == applicantId);
        }
    }
} 