using System;

namespace RepositoryAndUnitOfWork.Utilities
{
    public abstract class AuditableEntity<TKey> 
    {

        public virtual TKey Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
    }
    public abstract class AuditableEntity : AuditableEntity<int> { }
}