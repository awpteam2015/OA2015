using System;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface
{
    public interface IModificationAudited
    {
        string LastModifierUserCode { get; set; }

        DateTime? LastModificationTime { get; set; }

    }
}
