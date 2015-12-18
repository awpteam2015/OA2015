using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing
{
    public interface IDeletionAudited
    {

        string DeleterUserCode { get; set; }


        DateTime? DeletionTime { get; set; }
    }
}
