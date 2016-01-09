

 /***************************************************************************
 *       功能：     HRGroupEmployee实体类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     组成员情况
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class GroupEmployeeEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String GroupCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String EmployeeCode{get; set;}
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
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

