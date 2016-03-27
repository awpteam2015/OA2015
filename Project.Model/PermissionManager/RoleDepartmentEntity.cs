

 /***************************************************************************
 *       功能：     PMRoleDepartment实体类
 *       作者：     Roy
 *       日期：     2016/3/27
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.PermissionManager
{
    public class RoleDepartmentEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32 RoleId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32 DepartId{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

