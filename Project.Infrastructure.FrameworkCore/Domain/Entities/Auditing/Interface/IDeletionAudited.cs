using System;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface
{
    public interface IDeletionAudited
    {
        string DeleterUserCode { get; set; }

        DateTime? DeletionTime { get; set; }
    }
}
