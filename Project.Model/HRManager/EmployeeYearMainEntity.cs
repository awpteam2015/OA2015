

/***************************************************************************
*       功能：     HREmployeeYearMain实体类
*       作者：     Roy
*       日期：     2016-01-22
*       描述：     员工年休管理
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using Project.Model.PermissionManager;

namespace Project.Model.HRManager
{
    public class EmployeeYearMainEntity : Entity, ISoftDelete, IHasRemark
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
        public virtual System.DateTime? CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        public virtual bool IsDeleted { get; set; }
        #endregion


        #region 新增属性

        /// <summary>
        /// 部门
        /// </summary>
        public virtual DepartmentEntity DepartmentEntity { get; set; }

        #endregion
    }
}




