﻿
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.PermissionManager
{
     [Serializable]
    public class UserInfoEntity : AuditedEntity, ISoftDelete
    {
          
        #region 属性
        /// <summary>
        /// 数据库类型：System.String   大小:36 
        /// 描述:员工号
        /// </summary>
        public virtual System.String UserCode { get; set; }
        /// <summary>
        /// 数据库类型：System.String   大小:32 
        /// 描述:密码
        /// </summary>
        public virtual System.String Password { get; set; }
        /// <summary>
        /// 数据库类型：System.String   大小:100 
        /// 描述:用户名
        /// </summary>
        public virtual System.String UserName { get; set; }
        /// <summary>
        /// 数据库类型：System.String   大小:100 
        /// 描述:电子邮件
        /// </summary>
        public virtual System.String Email { get; set; }
        /// <summary>
        /// 数据库类型：System.String   大小:32 
        /// 描述:手机号
        /// </summary>
        public virtual System.String Mobile { get; set; }
        /// <summary>
        /// 数据库类型：System.String   大小:32 
        /// 描述:家庭电话
        /// </summary>
        public virtual System.String Tel { get; set; }
        /// <summary>
        /// 数据库类型：System.Int32   大小:4 精度:10
        /// 描述:是否有效
        /// </summary>
        public virtual System.Int32? IsActive { get; set; }

        /// <summary>
        /// 数据库类型：System.String   大小:255 
        /// 描述:备注
        /// </summary>
        public virtual System.String Remark { get; set; }
        #endregion


        public virtual bool IsDeleted { get; set; }
    }
}