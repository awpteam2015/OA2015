using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.ReportManager
{
    public class HREmployeeViewEntity
    {
        public virtual System.String EmployeeCode { get; set; }
        public virtual System.String EmployeeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String DepartmentName { get; set; }

        public virtual System.String InDepartmentName { get; set; }

        public virtual int? Sex { get; set; }

        public virtual System.String CertNo { get; set; }

        public virtual System.DateTime? Birthday { get; set; }

        public virtual System.String EmployeeTypeName { get; set; }

        public virtual System.String WorkStateName { get; set; }

        public virtual System.String InWorkStateName { get; set; }

        public virtual int? IsDeleted { get; set; }

        public virtual System.DateTime? CreationTime { get; set; }

        public virtual System.DateTime? CreationTimeEnd { get; set; }

        /// <summary>
        /// 进出类型  0：进 1：出
        /// </summary>
        public virtual System.Int32 InOrOut { get; set; }

    }
}
