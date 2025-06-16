using System.Threading.Tasks;

namespace Bootcamp.Core.Repositories
{
    public interface IBootcampRepository : IRepository<Entities.BootcampEntity>
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> IsActiveAsync(int bootcampId);
    }
} 