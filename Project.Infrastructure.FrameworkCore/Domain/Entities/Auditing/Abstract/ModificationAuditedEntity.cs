using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Abstract
{
     [Serializable]
    public abstract class ModificationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IModificationAudited
    {
         public virtual string LastModifierUserCode { get; set; }
         public virtual DateTime? LastModificationTime { get; set; }
    }

 

}
