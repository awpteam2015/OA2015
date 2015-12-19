using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Abstract;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities
{
    /// <summary>
    /// Add
    /// </summary>
     [Serializable]
    public abstract class CreationAuditedEntity : CreationAuditedEntity<int>
    {

    }

}
