

 /***************************************************************************
 *       功能：     HRGroup实体类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     用于管理组
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class GroupEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String GroupCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String GroupName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? Sort{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Remark{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CreatorUserCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CreatorUserName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? CreateTime{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? LastModificationTime{get; set;}
        /// <summary>
        /// 1:删除 0:未删除,默认0
        /// </summary>
        public virtual System.Int32? IsDeleted{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

