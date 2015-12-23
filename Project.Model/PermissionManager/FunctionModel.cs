

 /***************************************************************************
 *       功能：     PMFunction实体类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     模块功能
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.PermissionManager
{
    public class FunctionEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual System.String FunctionnName{get; set;}
        /// <summary>
        /// 模块ID
        /// </summary>
        public virtual System.Int32? ModuleId{get; set;}
        /// <summary>
        /// 模块路径
        /// </summary>
        public virtual System.String FunctionUrl{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Area{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Controller{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Action{get; set;}
        /// <summary>
        /// 是否在菜单上显示1是 0不是
        /// </summary>
        public virtual System.Int32? IsDisplayOnMenu{get; set;}
        /// <summary>
        /// 顺序
        /// </summary>
        public virtual System.Int32? RankId{get; set;}
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

