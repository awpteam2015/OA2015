

 /***************************************************************************
 *       功能：     HRYGWage实体类
 *       作者：     Roy
 *       日期：     2016-05-08
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class YGWageEntity: Entity, IAudited
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? EmployeeID{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String GWGZ{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String XZGZ{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CreatorUserCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CreattorUserName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? CreationTime{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? LastModificationTime{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String LastModifierUserCode{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

