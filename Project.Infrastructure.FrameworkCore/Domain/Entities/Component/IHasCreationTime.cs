using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Component
{
    /// <summary>
    ///例子
    /// </summary>
   public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; }
    }
}
