
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.PermissionManager
{
    [Serializable]
    public class UserInfoEntity : AuditedEntity, ISoftDelete
    {
        public UserInfoEntity()
        {
            UserDepartmentList = new HashSet<UserDepartmentEntity>();
            UserRoleList = new HashSet<UserRoleEntity>();
            UserFunctionDetailList = new HashSet<UserFunctionDetailEntity>();
        }

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

        public virtual ISet<UserDepartmentEntity> UserDepartmentList { get; set; }

        public virtual ISet<UserRoleEntity> UserRoleList { get; set; }


        /// <summary>
        /// 在角色基础上的增删详细模块功能
        /// </summary>
        public virtual ISet<UserFunctionDetailEntity> UserFunctionDetailList { get; set; }

        /// <summary>
        /// 有效权限
        /// </summary>
        public virtual IList<int> UserFunctionDetailList_Checked { get; set; }

    }
}
