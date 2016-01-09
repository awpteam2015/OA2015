

 /***************************************************************************
 *       功能：     HREmployeeChildren实体类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     职工子女登记
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class EmployeeChildrenEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String EmployeeCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ChildrenName{get; set;}
        /// <summary>
        /// 0:女 1:男
        /// </summary>
        public virtual System.Int32? Sex{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Relation{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Certificate{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? JoinDate{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Hospital{get; set;}
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

    
 

