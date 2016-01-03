

 /***************************************************************************
 *       功能：     PMUserRole实体类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     EC系统用户和角色对应关系表
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.PermissionManager
{
    public class UserRoleEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual System.String UserCode{get; set;}
        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual System.Int32 RoleId{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

