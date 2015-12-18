using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Abstract
{
     [Serializable]
    public abstract class ModificationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IModificationAudited
    {
         public string LastModifierUserCode { get; set; }
         public DateTime? LastModificationTime { get; set; }
    }

 

}
