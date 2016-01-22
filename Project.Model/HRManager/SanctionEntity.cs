

/***************************************************************************
*       功能：     HRSanction实体类
*       作者：     Roy
*       日期：     2016-01-18
*       描述：     职工奖罚及部门等奖罚
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class SanctionEntity : Entity
    {
        #region 属性
        /// <summary>
        /// 奖罚类型
        /// </summary>
        public virtual System.Int32? SanctionType { get; set; }
        /// <summary>
        /// 奖罚对象类型
        /// </summary>
        public virtual System.Int32? SanctionObjType { get; set; }
        /// <summary>
        /// 奖励等级
        /// </summary>
        public virtual System.Int32? SanctionObjLevel { get; set; }
        /// <summary>
        /// 等级名称
        /// </summary>
        public virtual System.String SanctionObjLevelName { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }
        /// <summary>
        /// 奖罚对象Code
        /// </summary>
        public virtual System.String SanctionObjName { get; set; }
        /// <summary>
        /// 奖罚对象
        /// </summary>
        public virtual System.String SanctionObj { get; set; }
        /// <summary>
        /// 奖罚名目
        /// </summary>
        public virtual System.String SanctionTitle { get; set; }
        /// <summary>
        /// 奖罚金额
        /// </summary>
        public virtual System.Decimal? SanctionMoney { get; set; }
        /// <summary>
        /// 奖罚日期
        /// </summary>
        public virtual System.DateTime? SanctionDate { get; set; }
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
        /// 创建日期
        /// </summary>
        public virtual System.DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        #endregion


        #region 新增属性

        public virtual System.DateTime? SanctionDateEnd { get; set; }
        #endregion
    }
}




