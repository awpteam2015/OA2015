using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Abstract;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities
{
    /// <summary>
    /// Add Update
    /// </summary>
     [Serializable]
    public abstract class AuditedEntity : AuditedEntity<int>
    {

    }
}
