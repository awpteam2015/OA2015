

 /***************************************************************************
 *       功能：     HRGoAbroad实体类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     记录人员出国情况
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class GoAbroadEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String EmployeeCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String DepartmentCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Country{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? BeginDate{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? EndDate{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? DaySum{get; set;}
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
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

