using System;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface
{
    /// <summary>
    /// 新增授权
    /// </summary>
    public interface ICreationAudited
    {
  
        string CreatorUserCode { get; set; }

        DateTime? CreationTime { get; set; }
    }
}
