using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing
{
    public interface IFullAudited:IAudited,IDeletionAudited
    {
    }
}
