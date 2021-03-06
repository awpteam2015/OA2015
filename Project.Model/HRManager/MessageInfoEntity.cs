﻿

/***************************************************************************
*       功能：     SysMessageInfo实体类
*       作者：     Roy
*       日期：     2016-02-21
*       描述：     站内短信
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class MessageInfoEntity : Entity
    {
        #region 属性
        /// <summary>
        /// 标题
        /// </summary>
        public virtual System.String MesTitle { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public virtual System.String MesContent { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public virtual System.String ReceiveUserCode { get; set; }
        /// <summary>
        /// 是否所有人
        /// </summary>
        public virtual System.Int32? IsAll { get; set; }

        /// <summary>
        /// 类型 1：生日提醒 2：退休提醒 3：合同过期提醒 4：合同到期提醒 5：其它提醒
        /// </summary>
        public virtual System.Int32? InfoType { get; set; }
        /// <summary>
        /// 已读人员
        /// </summary>
        public virtual System.String ReadUser { get; set; }
        /// 发送人
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 发送姓名
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
        public virtual System.String LastModifierUserCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String DeleterUserCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? DeletionTime { get; set; }
        #endregion


        #region 新增属性

        /// <summary>
        /// 查询专用
        /// </summary>
        public virtual System.DateTime? CreationTimeEnd { get; set; }

        /// <summary>
        /// 当前人员编号
        /// </summary>
        public virtual System.String CurUserCode { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public virtual System.Boolean? IsRead { get; set; }

        /// <summary>
        /// 用于查未读人员
        /// </summary>
        public virtual  System.String SreachNoReadUserCode { get; set; }
        #endregion
    }
}




