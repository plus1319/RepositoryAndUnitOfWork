using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepositoryAndUnitOfWork.Utilities;

namespace RepositoryAndUnitOfWork.Models
{
    public class User : AuditableEntity
    {
        public string Name { get; set; }
    }
}
