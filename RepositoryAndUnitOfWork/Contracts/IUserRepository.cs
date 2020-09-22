using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepositoryAndUnitOfWork.Models;
using RepositoryAndUnitOfWork.Repositories;

namespace RepositoryAndUnitOfWork.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllAsync();
    }
}