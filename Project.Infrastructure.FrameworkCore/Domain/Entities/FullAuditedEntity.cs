using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Abstract;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities
{
     [Serializable]
    public abstract class FullAuditedEntity : FullAuditedEntity<int>
    {
    }
}
