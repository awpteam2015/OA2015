using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Component
{
    public interface IMustHaveSiteCode
    {
        string SiteCode { get; set; }
    }
}
