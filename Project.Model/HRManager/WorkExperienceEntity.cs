

 /***************************************************************************
 *       功能：     HRWorkExperience实体类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     员工工作经历记录
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class WorkExperienceEntity: Entity
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
        public virtual System.String WorkCompany{get; set;}
        /// <summary>
        /// 单位职务（职位）
        /// </summary>
        public virtual System.String Duties{get; set;}
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
        public virtual System.String WorkContent{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String LeaveReason{get; set;}
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

    
 

