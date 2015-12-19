using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Abstract
{
    public abstract class FullAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IFullAudited
    {
        public virtual string CreatorUserCode { get; set; }
        public virtual DateTime? CreationTime { get; set; }
        public virtual string LastModifierUserCode { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual string DeleterUserCode { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
    }
}
