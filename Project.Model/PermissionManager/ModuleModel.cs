

 /***************************************************************************
 *       功能：     PMModule实体类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     权限模块
 * *************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.PermissionManager
{
    public class ModuleEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual System.String ModuleName{get; set;}
        /// <summary>
        /// 父级 预留
        /// </summary>
        public virtual System.Int32? ParentId{get; set;}
        /// <summary>
        /// 层级
        /// </summary>
        public virtual System.Int32? ModuleLevel{get; set;}
        /// <summary>
        /// 排序
        /// </summary>
        public virtual System.Int32? RankId{get; set;}
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark{get; set;}
		#endregion
        

        #region 新增属性

        public virtual ISet<FunctionEntity> FunctionEntityList { get; set; }

        #endregion
    }
}

    
 

