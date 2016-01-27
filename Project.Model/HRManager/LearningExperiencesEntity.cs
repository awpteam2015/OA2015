

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
    public class LearningExperiencesEntity : Entity, IHasRemark, IAudited
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32 EmployeeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public virtual System.String ProfessionCode { get; set; }
        /// <summary>
        /// 学校
        /// </summary>
        public virtual System.String School { get; set; }
        /// <summary>
        /// 学位
        /// </summary>
        public virtual System.String Degree { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public virtual System.String Education { get; set; }
        /// <summary>
        /// 学制
        /// </summary>
        public virtual System.String SchoolYear { get; set; }
        /// <summary>
        /// 证书编号
        /// </summary>
        public virtual System.String CertNumber { get; set; }
        /// <summary>
        /// 入学日期
        /// </summary>
        public virtual System.DateTime? BeginDate { get; set; }
        /// <summary>
        /// 毕业日期
        /// </summary>
        public virtual System.DateTime? EndDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark { get; set; }
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




