

 /***************************************************************************
 *       功能：     HRLearningExperiences实体类
 *       作者：     Roy
 *       日期：     2016-01-17
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
        /// 员工编号
        /// </summary>
        public virtual System.String EmployeeCode{get; set;}
        /// <summary>
        /// 部门编号
        /// </summary>
        public virtual System.String DepartmentCode{get; set;}
        /// <summary>
        /// 专业
        /// </summary>
        public virtual System.String ProfessionCode{get; set;}
        /// <summary>
        /// 学校
        /// </summary>
        public virtual System.String School{get; set;}
        /// <summary>
        /// 获取学位
        /// </summary>
        public virtual System.String Degree{get; set;}
        /// <summary>
        /// 获取学历
        /// </summary>
        public virtual System.String Education{get; set;}
        /// <summary>
        /// 证明人
        /// </summary>
        public virtual System.String VerifyPersone{get; set;}
        /// <summary>
        /// 获奖说明
        /// </summary>
        public virtual System.String Reward{get; set;}
        /// <summary>
        /// 获奖证书
        /// </summary>
        public virtual System.String Certificate{get; set;}
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark{get; set;}
        /// <summary>
        /// 操作人员
        /// </summary>
        public virtual System.String CreatorUserCode{get; set;}
        /// <summary>
        /// 操作人员名称
        /// </summary>
        public virtual System.String CreatorUserName{get; set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreateTime{get; set;}
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

