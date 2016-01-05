

 /***************************************************************************
 *       功能：     PMFunctionDetail实体类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     模块功能点
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.PermissionManager
{
    public class FunctionDetailEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 功能名称
        /// </summary>
        public virtual System.String FunctionDetailName{get; set;}
        /// <summary>
        /// 功能代号对应页面需要控制的按钮Id
        /// </summary>
        public virtual System.String FunctionDetailCode{get; set;}
        /// <summary>
        /// 模块ID
        /// </summary>
        public virtual System.Int32 FunctionId{get; set;}

        public virtual System.Int32 ModuleId { get; set; }
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
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode{get; set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime CreationTime{get; set;}
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode{get; set;}
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime{get; set;}
		#endregion
        

        #region 新增属性
        public virtual bool Attr_IsCheck { get; set; }
        #endregion
    }
}

    
 

