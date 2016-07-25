

 /***************************************************************************
 *       功能：     RMRiverOwer实体类
 *       作者：     李伟伟
 *       日期：     2016/7/24
 *       描述：     河道河长绑定关系
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.RiverManager
{
    public class RiverOwerEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? RiverId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String RiverName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserName{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

