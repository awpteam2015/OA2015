using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Abstract
{
    public abstract class AuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IAudited
    {
        public virtual string CreatorUserCode { get; set; }
        public virtual DateTime? CreationTime { get; set; }
        public virtual string LastModifierUserCode { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
    }
}
