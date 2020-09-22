using Microsoft.EntityFrameworkCore;
using RepositoryAndUnitOfWork.Models;

namespace RepositoryAndUnitOfWork.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }


    }
}