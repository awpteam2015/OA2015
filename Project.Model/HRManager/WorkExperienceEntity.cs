

/***************************************************************************
*       功能：     HRWorkExperience实体类
*       作者：     Roy
*       日期：     2016-01-17
*       描述：     员工工作经历记录
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class WorkExperienceEntity : Entity
    {
        #region 属性
        /// <summary>
        /// 员工编号
        /// </summary>
        public virtual System.Int32 EmployeeID { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public virtual System.String WorkCompany { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public virtual System.String Duties { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public virtual System.DateTime? BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual System.DateTime? EndDate { get; set; }
        /// <summary>
        /// 工作内容
        /// </summary>
        public virtual System.String WorkContent { get; set; }
        /// <summary>
        /// 离职原因
        /// </summary>
        public virtual System.String LeaveReason { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 操作人名称
        /// </summary>
        public virtual System.String CreatorUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}




