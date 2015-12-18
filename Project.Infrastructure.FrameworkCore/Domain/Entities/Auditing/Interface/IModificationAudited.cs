using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing
{
    public interface IModificationAudited
    {
        string LastModifierUserCode { get; set; }

        DateTime? LastModificationTime { get; set; }

    }
}
