using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Abstract
{
    public abstract class FullAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IFullAudited
    {
        public string CreatorUserCode { get; set; }
        public DateTime? CreationTime { get; set; }
        public string LastModifierUserCode { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string DeleterUserCode { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
