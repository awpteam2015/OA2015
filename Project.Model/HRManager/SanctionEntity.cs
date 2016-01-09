

 /***************************************************************************
 *       功能：     HRSanction实体类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     职工奖罚及部门等奖罚
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class SanctionEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 0:罚 1:奖
        /// </summary>
        public virtual System.Int32? SanctionType{get; set;}
        /// <summary>
        /// 0:个人 1:部门（科室）
        /// </summary>
        public virtual System.Int32? SanctionObj{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String SanctionTitle{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Decimal? SanctionMoney{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? SanctionDate{get; set;}
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
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

