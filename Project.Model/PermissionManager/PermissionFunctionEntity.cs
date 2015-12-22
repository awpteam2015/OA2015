using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Domain.Entities;

namespace Project.Model.PermissionManager
{
    public class PermissionFunctionEntity:Entity
    {
        public string FunctionCode { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
