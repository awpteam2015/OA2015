

/***************************************************************************
*       功能：     HRLearningExperiences实体类
*       作者：     Roy
*       日期：     2016-01-17
*       描述：     员工学习经历
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;

namespace Project.Model.HRManager
{
    public class YearAssessmentEntity : Entity, IAudited
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? EmployeeID { get; set; }

        public virtual System.String KHYear { get; set; }
        public virtual System.String KHComment { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CreatorUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}




