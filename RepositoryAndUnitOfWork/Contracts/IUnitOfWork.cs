using System;
using System.Threading.Tasks;
using RepositoryAndUnitOfWork.Repositories;

namespace RepositoryAndUnitOfWork.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        void Commit();
        Task<int> CommitAsync();
    }
}