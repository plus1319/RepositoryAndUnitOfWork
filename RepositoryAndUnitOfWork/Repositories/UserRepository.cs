using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryAndUnitOfWork.Contracts;
using RepositoryAndUnitOfWork.Data;
using RepositoryAndUnitOfWork.Models;

namespace RepositoryAndUnitOfWork.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }
    }
}