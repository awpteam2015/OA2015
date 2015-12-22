

 /***************************************************************************
 *       功能：     PMDepartment实体类
 *       作者：     李伟伟
 *       日期：     2015/12/22
 *       描述：     部门基础信息表
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.PermissionManager
{
    public class DepartmentEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 部门编码
        /// </summary>
        public virtual System.String DepartmentCode{get; set;}
        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual System.String DepartmentName{get; set;}
        /// <summary>
        /// 公司代码
        /// </summary>
        public virtual System.String ParentdepartmentCode{get; set;}
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

