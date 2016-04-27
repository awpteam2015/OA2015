using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;

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

        public virtual System.String Duties { get; set; }

        /// <summary>
        /// 单位职务
        /// </summary>
        public virtual System.String DutiesName { get; set; }

        public virtual System.String CertNo { get; set; }

        public virtual System.DateTime? Birthday { get; set; }
        public virtual System.String EmployeeType { get; set; }
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

        /// <summary>
        ///  岗位等级
        /// </summary>
        public virtual System.String PostLevel { get; set; }

        /// <summary>
        /// 岗位等级名称 
        /// </summary>
        public virtual System.String PostLevelName { get; set; }
        /// <summary>
        /// 岗位性质
        /// </summary>
        public virtual System.String PostProperty { get; set; }

        /// <summary>
        ///  岗位性质名称
        /// </summary>
        public virtual System.String PostPropertyName { get; set; }

        /// <summary>
        /// 入党时间 
        /// </summary>
        public virtual System.DateTime? JoinCommy { get; set; }


        /// <summary>
        /// 是否党员 
        /// </summary>
        public virtual System.Int32 IsCommy { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public virtual System.String Education { get; set; }

        /// <summary>
        /// 学历名称
        /// </summary>
        public virtual System.String EducationName { get; set; }

        public virtual long __hibernate_sort_row
        {
            set;
            get;
        }

        public virtual System.DateTime? CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public virtual System.String PoliticsName { get; set; }
    }
}
