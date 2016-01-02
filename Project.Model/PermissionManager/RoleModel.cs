

/***************************************************************************
*       功能：     PMRole实体类
*       作者：     李伟伟
*       日期：     2015/12/23
*       描述：     EC系统角色列表
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.PermissionManager
{
    public class RoleEntity : Entity
    {
        #region 属性
        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual System.String RoleName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark { get; set; }
        #endregion


        #region 新增属性
        public virtual bool Attr_IsCheck { get; set; }
        public virtual int Attr_UserRolePkId { get; set; }
        #endregion
    }
}




