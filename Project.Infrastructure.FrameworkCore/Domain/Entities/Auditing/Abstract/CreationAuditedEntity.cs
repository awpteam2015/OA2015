using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Abstract
{
     [Serializable]
    public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited
    {
         public virtual string CreatorUserCode { get; set; }
         public virtual DateTime? CreationTime { get; set; }
    }

   

}
