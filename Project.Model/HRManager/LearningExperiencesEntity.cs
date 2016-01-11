

 /***************************************************************************
 *       功能：     HRLearningExperiences实体类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     员工学习经历
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class LearningExperiencesEntity: Entity
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
        /// 数据字典维护
        /// </summary>
        public virtual System.String ProfessionCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String School{get; set;}
        /// <summary>
        /// 手动输入
        /// </summary>
        public virtual System.String Degree{get; set;}
        /// <summary>
        /// 手动输入
        /// </summary>
        public virtual System.String Education{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String VerifyPersone{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Reward{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Certificate{get; set;}
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

    
 

