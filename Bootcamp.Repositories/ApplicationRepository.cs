using Bootcamp.Core.Repositories;
using Bootcamp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bootcamp.Repositories
{
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        public ApplicationRepository(BootcampDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByApplicantAndBootcampAsync(int applicantId, int bootcampId)
        {
            return await _dbSet.AnyAsync(a => a.ApplicantId == applicantId && a.BootcampId == bootcampId);
        }

        public async Task<bool> CanUpdateStatusAsync(int applicationId, ApplicationState newState)
        {
            var application = await _dbSet.FindAsync(applicationId);
            if (application == null)
                return false;

            // Define valid state transitions
            switch (application.ApplicationState)
            {
                case ApplicationState.PENDING:
                    return newState == ApplicationState.APPROVED || 
                           newState == ApplicationState.REJECTED || 
                           newState == ApplicationState.IN_REVIEW;
                case ApplicationState.IN_REVIEW:
                    return newState == ApplicationState.APPROVED || 
                           newState == ApplicationState.REJECTED;
                case ApplicationState.APPROVED:
                case ApplicationState.REJECTED:
                case ApplicationState.CANCELLED:
                    return false; // Final states cannot be changed
                default:
                    return false;
            }
        }
    }
} 