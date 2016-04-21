

/***************************************************************************
*       功能：     HREmployeeFile实体类
*       作者：     Roy
*       日期：     2016/4/21
*       描述：     
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class EmployeeFileEntity : Entity,IAudited
    {
        #region 属性
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual System.Int32? EmployeeID { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public virtual System.String FName { get; set; }

        /// <summary>
        /// 原文件名
        /// </summary>
        public virtual System.String FOrgName { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        public virtual System.String FileUrl { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public virtual System.String CreattorUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String LastModifierUserCode { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}




