using System.Threading.Tasks;

namespace Bootcamp.Core.Repositories
{
    public interface IApplicationRepository : IRepository<Entities.Application>
    {
        Task<bool> ExistsByApplicantAndBootcampAsync(int applicantId, int bootcampId);
        Task<bool> CanUpdateStatusAsync(int applicationId, Entities.ApplicationState newState);
    }
} 