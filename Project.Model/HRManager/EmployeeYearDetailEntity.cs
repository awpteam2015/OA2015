

/***************************************************************************
*       功能：     HREmployeeYearDetail实体类
*       作者：     Roy
*       日期：     2016-01-22
*       描述：     年休登记
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;

namespace Project.Model.HRManager
{
    public class EmployeeYearDetailEntity : Entity, ISoftDelete, IHasRemark, IAudited
    {
        #region 属性
        /// <summary>
        /// 部门编号
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public virtual System.String EmployeeCode { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public virtual System.String EmployeeName { get; set; }
        /// <summary>
        /// 0 添加登记  1：使用登记
        /// </summary>
        public virtual System.Int32? UseType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? BeginDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? EndDate { get; set; }
        /// <summary>
        /// _decimal合计天数
        /// </summary>
        public virtual System.Decimal? UseCount { get; set; }
        /// <summary>
        /// _decimal使用前天数
        /// </summary>
        public virtual System.Decimal? BeforeUseCount { get; set; }
        /// <summary>
        /// _decimal 年休余数
        /// </summary>
        public virtual System.Decimal? LeftCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CreatorUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string LastModifierUserCode { get; set; }
        public virtual bool IsDeleted { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}




