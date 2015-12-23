

 /***************************************************************************
 *       功能：     PMUserDepartment实体类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     用户所属部门
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.PermissionManager
{
    public class UserDepartmentEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 用户ID（工号）
        /// </summary>
        public virtual System.String UserCode{get; set;}
        /// <summary>
        /// 部门代码
        /// </summary>
        public virtual System.String DepartmentCode{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

