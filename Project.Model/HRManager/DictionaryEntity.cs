

 /***************************************************************************
 *       功能：     SMDictionary实体类
 *       作者：     ROY
 *       日期：     2016-01-10
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class DictionaryEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String KeyCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ParentKeyCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String KeyName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String KeyValue{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

