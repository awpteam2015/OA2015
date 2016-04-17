using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.ReportManager
{
  public  class AttendanceViewEntity
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

        /// <summary>
        /// 日
        /// </summary>
        public virtual System.Int32 RiDays { get; set; }

        /// <summary>
        /// 夜
        /// </summary>
        public virtual System.Int32 YeDays { get; set; }

        /// <summary>
        /// 公
        /// </summary>
        public virtual System.Int32 GongDays { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public virtual System.Int32 ZhiDays { get; set; }

        /// <summary>
        /// 缺
        /// </summary>
        public virtual System.Int32 QueDays { get; set; }


        public virtual DateTime? Attr_StartDate { get; set; }

        public virtual DateTime? Attr_EndDate { get; set; }
        public virtual long __hibernate_sort_row
        {
            set;
            get;
        }
    }
}
