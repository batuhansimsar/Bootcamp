using System.Threading.Tasks;

namespace Bootcamp.Core.Repositories
{
    public interface IUserRepository<T> : IRepository<T> where T : class
    {
        Task<T> GetByEmailAsync(string email);
        Task<bool> ExistsByNationalityIdentityAsync(string nationalityIdentity);
    }
} 