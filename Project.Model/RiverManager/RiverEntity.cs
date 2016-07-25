

/***************************************************************************
*       功能：     RMRiver实体类
*       作者：     李伟伟
*       日期：     2016/7/23
*       描述：     河道信息
* *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.RiverManager
{
    public class RiverEntity : Entity, IAudited
    {
        #region 属性
        /// <summary>
        /// 河道名称
        /// </summary>
        public virtual System.String RiverName { get; set; }
        /// <summary>
        /// 河道等级
        /// </summary>
        public virtual System.String RiverRank { get; set; }
        /// <summary>
        /// 河道范围
        /// </summary>
        public virtual System.String RiverArea { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public virtual System.String RiverLength { get; set; }
        /// <summary>
        /// 流经乡（镇）
        /// </summary>
        public virtual System.String RiverCrossArea { get; set; }
        /// <summary>
        /// 坐标
        /// </summary>
        public virtual System.String Coords { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual System.Int32? IsActive { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark { get; set; }

        public virtual System.String DepartmentCode { get; set; }
        public virtual System.String DepartmentName { get; set; }

        public virtual ISet<RiverOwerEntity> RiverOwerList { get; set; }

        #endregion


        #region 新增属性

        public virtual bool Attr_IsCheck { get; set; }
        public virtual int Attr_RiverOwerPkId { get; set; }


        #endregion
    }
}




