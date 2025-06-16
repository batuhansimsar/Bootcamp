using System.Threading.Tasks;

namespace Bootcamp.Core.Repositories
{
    public interface IBlacklistRepository : IRepository<Entities.Blacklist>
    {
        Task<bool> ExistsByApplicantIdAsync(int applicantId);
    }
} 