using Bootcamp.Core.Repositories;
using Bootcamp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bootcamp.Repositories
{
    public class UserRepository<T> : Repository<T>, IUserRepository<T> where T : User
    {
        public UserRepository(BootcampDbContext context) : base(context)
        {
        }

        public async Task<T> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByNationalityIdentityAsync(string nationalityIdentity)
        {
            return await _dbSet.AnyAsync(u => u.NationalityIdentity == nationalityIdentity);
        }
    }
} 