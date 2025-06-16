using System.Threading.Tasks;

namespace Bootcamp.Core.Repositories
{
    public interface IApplicantRepository : IUserRepository<Entities.Applicant>
    {
        Task<bool> IsBlacklistedAsync(int applicantId);
    }
} 