

/***************************************************************************
*       功能：     HRProfession实体类
*       作者：     Roy
*       日期：     2016-02-02
*       描述：     职业资格
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class ProfessionEntity : Entity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? EmployeeID { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public virtual System.String EmployeeCode { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual System.String Title { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public virtual System.String TypeName { get; set; }
        /// <summary>
        /// 范围
        /// </summary>
        public virtual System.String RangeName { get; set; }
        /// <summary>
        /// 取得时间
        /// </summary>
        public virtual System.DateTime? GetDate { get; set; }
        /// <summary>
        /// 职称证书编号
        /// </summary>
        public virtual System.String CerNo { get; set; }
        /// <summary>
        /// 操作人员
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 操作人员名称
        /// </summary>
        public virtual System.String CreatorUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode { get; set; }

        /// <summary>
        /// 聘用时间
        /// </summary>
        public virtual System.DateTime? EmployDate { get; set; }
        /// <summary>
        /// 聘用结束时间
        /// </summary>
        public virtual System.DateTime? EmployEndDate { get; set; }

        #endregion


        #region 新增属性

        #endregion
    }
}




