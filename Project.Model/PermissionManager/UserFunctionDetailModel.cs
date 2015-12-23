

 /***************************************************************************
 *       功能：     PMUserFunctionDetail实体类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     用户对应的权限
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.PermissionManager
{
    public class UserFunctionDetailEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserCode{get; set;}
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

    
 

