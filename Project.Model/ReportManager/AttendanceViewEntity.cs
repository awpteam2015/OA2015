using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.ReportManager
{
  public  class AttendanceViewEntity2
    {
        public virtual System.String EmployeeCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String DepartmentName { get; set; }

        public virtual System.Int32 WordkDays { get; set; }

        public virtual System.Int32 NotWordkDays { get; set; }

        public virtual System.Int32 EmployeeNum { get; set; }

        public virtual DateTime? Attr_StartDate { get; set; }

        public virtual DateTime? Attr_EndDate { get; set; }
        public virtual long __hibernate_sort_row
        {
            set;
            get;
        }
    }
}
