

/***************************************************************************
*       功能：     HRGoAbroad实体类
*       作者：     Roy
*       日期：     2016-01-19
*       描述：     记录人员出国情况
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class GoAbroadEntity : Entity
    {
        #region 属性
        /// <summary>
        /// 员工编号
        /// </summary>
        public virtual System.String EmployeeCode { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public virtual System.String EmployeeName { get; set; }
        /// <summary>
        /// 员工部门
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual System.String DepartmentName { get; set; }
        /// <summary>
        /// 出差国家
        /// </summary>
        public virtual System.String Country { get; set; }
        /// <summary>
        /// 出国开始日期
        /// </summary>
        public virtual System.DateTime? BeginDate { get; set; }
        /// <summary>
        /// 出国结束日期
        /// </summary>
        public virtual System.DateTime? EndDate { get; set; }
        /// <summary>
        /// 出国天数
        /// </summary>
        public virtual System.Int32? DaySum { get; set; }
        /// <summary>
        /// 事由
        /// </summary>
        public virtual System.String Reason { get; set; }
        /// <summary>
        /// 备注
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
        #endregion


        #region 新增属性
        /// <summary>
        /// 数据库无此字段，用于查询操作
        /// </summary>
        public virtual System.DateTime? CreateTimeEnd { get; set; }
        #endregion
    }
}




