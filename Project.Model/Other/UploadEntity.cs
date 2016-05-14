using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Domain.Entities;

namespace Project.Model.Other
{
   public class UploadEntity : IEntity<int>
   {
       public string FileUrl { get; set; }
        public string FileName { get; set; }

       #region Implementation of IEntity<int>

       public int PkId { get; set; }
       public int Version { get; set; }

       #endregion
   }
}
