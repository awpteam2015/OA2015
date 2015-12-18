using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Abstract;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities
{
     [Serializable]
    public abstract class ModificationAuditedEntity : ModificationAuditedEntity<int>
    {

    }

 

}
