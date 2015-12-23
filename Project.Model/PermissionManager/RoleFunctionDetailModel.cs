

 /***************************************************************************
 *       功能：     PMRoleFunctionDetail实体类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     角色对应的权限
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.PermissionManager
{
    public class RoleFunctionDetailEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? RoleId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? FunctionId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? FunctionDetailId{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

