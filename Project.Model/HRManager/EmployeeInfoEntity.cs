

/***************************************************************************
*       功能：     HREmployeeInfo实体类
*       作者：     ROY
*       日期：     2016-01-11
*       描述：     通过FSate字段进行过滤是还是历史记录
  人员基础信息，如需要增加多字段请使用扩展表
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;
using Project.Model.PermissionManager;

namespace Project.Model.HRManager
{
    public class EmployeeInfoEntity : Entity, ISoftDelete, IHasRemark, IAudited
    {
        public EmployeeInfoEntity()
        {
            this.WorkList = new HashSet<WorkExperienceEntity>();
            this.LearningList = new HashSet<LearningExperiencesEntity>();
            this.ContinEducationList = new HashSet<ContinEducationEntity>();
            this.TechnicalList = new HashSet<TechnicalEntity>();
            this.ProfessionList = new HashSet<ProfessionEntity>();
            this.YearAssessmentList = new HashSet<YearAssessmentEntity>();
            this.EmployeeFileList = new HashSet<EmployeeFileEntity>();
            this.WageList = new HashSet<YGWageEntity>();
            //this.DepartModel = new DepartmentEntity();
        }
        #region 属性
        /// <summary>
        /// 员工编号
        /// </summary>
        public virtual System.String EmployeeCode { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        public virtual System.String EmployeeName { get; set; }

        public virtual System.String EmployeeNameAndEmployeeCode
        {
            get { return PayCode + EmployeeName; }
        }

        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.String DepartmentName { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public virtual System.String JobName { get; set; }
        /// <summary>
        /// 中文简拼
        /// </summary>
        public virtual System.String PayCode { get; set; }
        /// <summary>
        /// 姓别
        /// </summary>
        public virtual System.Int32? Sex { get; set; }

        /// <summary>
        /// 性别中名
        /// </summary>
        public virtual System.String SexName { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual System.String CertNo { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public virtual System.DateTime? Birthday { get; set; }
        /// <summary>
        /// 技术职称名称
        /// </summary>
        public virtual System.String TechnicalTitleName { get; set; }
        /// <summary>
        /// 技术职称
        /// </summary>
        public virtual System.String TechnicalTitle { get; set; }
        /// <summary>
        /// 职务名称
        /// </summary>
        public virtual System.String DutiesName { get; set; }
        /// <summary>
        /// 单位职务
        /// </summary>
        public virtual System.String Duties { get; set; }
        /// <summary>
        /// 在职状态
        /// </summary>
        public virtual System.String WorkState { get; set; }
        /// <summary>
        /// 工龄
        /// </summary>
        public virtual System.Int32? WorkingYears { get; set; }
        /// <summary>
        /// 员工类型
        /// </summary>
        public virtual System.String EmployeeType { get; set; }
        /// <summary>
        /// 员工类型名称
        /// </summary>
        public virtual System.String EmployeeTypeName { get; set; }
        /// <summary>
        /// 家庭地址
        /// </summary>
        public virtual System.String HomeAddress { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public virtual System.String MobileNO { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public virtual System.String FileUrl { get; set; }

        public virtual System.String FileName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public virtual System.Int32? Sort { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual System.Int32? State { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 操作员名称
        /// </summary>
        public virtual System.String CreatorUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        public virtual string LastModifierUserCode { get; set; }

        //
        public virtual string LastModifierUserName { get; set; }
        /// <summary>
        /// 在职状态名称
        /// </summary>
        public virtual System.String WorkStateName { get; set; }

        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 开始工作时间
        /// </summary>
        public virtual DateTime? StartWork { get; set; }

        /// <summary>
        /// 入党时间
        /// </summary>
        public virtual DateTime? JoinCommy { get; set; }

        public virtual System.String PostLevel { get; set; }

        public virtual System.String PostLevelName { get; set; }

        public virtual System.String PostProperty { get; set; }
        public virtual System.String PostPropertyName { get; set; }


        public virtual System.String EngageInPost { get; set; }
        public virtual System.String EngageInPostName { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public virtual System.String PoliticsName { get; set; }
        /// <summary>
        /// 进入本单位时间
        /// </summary>
        public virtual System.DateTime? IntoCompanyTime { get; set; }

        #endregion


        #region 新增属性

        /// <summary>
        /// 工作经历
        /// </summary>
        public virtual ISet<WorkExperienceEntity> WorkList { get; set; }

        /// <summary>
        /// 学习经历
        /// </summary>
        public virtual ISet<LearningExperiencesEntity> LearningList { get; set; }
        /// <summary>
        /// 继续教育学分
        /// </summary>
        public virtual ISet<ContinEducationEntity> ContinEducationList { get; set; }
        /// <summary>
        /// 职务经历
        /// </summary>
        public virtual ISet<TechnicalEntity> TechnicalList { get; set; }

        /// <summary>
        /// 职业资格
        /// </summary>
        public virtual ISet<ProfessionEntity> ProfessionList { get; set; }

        /// <summary>
        /// 年度考核
        /// </summary>
        public virtual ISet<YearAssessmentEntity> YearAssessmentList { get; set; }

        /// <summary>
        /// 上传列表
        /// </summary>
        public virtual ISet<EmployeeFileEntity> EmployeeFileList { get; set; }

        /// <summary>
        /// 人事工资列表
        /// </summary>
        public virtual ISet<YGWageEntity> WageList { get; set; }
        //public virtual DepartmentEntity DepartModel { get; set; }
        #endregion
    }

    public class EmployeeInfoResponse
    {
        #region 属性
        /// <summary>
        /// 员工编号
        /// </summary>
        public virtual System.String EmployeeCode { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        public virtual System.String EmployeeName { get; set; }

        public virtual System.String EmployeeNameAndEmployeeCode
        {
            get { return PayCode + EmployeeName; }
        }

        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.String DepartmentName { get; set; }

        /// <summary>
        ///父级编号
        /// </summary>
        public virtual string PDepartmentCode { get; set; }

        /// <summary>
        ///父级名称
        /// </summary>
        public virtual string PDepartmentName { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public virtual System.String JobName { get; set; }
        /// <summary>
        /// 中文简拼
        /// </summary>
        public virtual System.String PayCode { get; set; }
        /// <summary>
        /// 姓别
        /// </summary>
        public virtual System.Int32? Sex { get; set; }

        /// <summary>
        /// 性别中名
        /// </summary>
        public virtual System.String SexName { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual System.String CertNo { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public virtual System.DateTime? Birthday { get; set; }
        /// <summary>
        /// 技术职称名称
        /// </summary>
        public virtual System.String TechnicalTitleName { get; set; }
        /// <summary>
        /// 技术职称
        /// </summary>
        public virtual System.String TechnicalTitle { get; set; }
        /// <summary>
        /// 职务名称
        /// </summary>
        public virtual System.String DutiesName { get; set; }
        /// <summary>
        /// 单位职务
        /// </summary>
        public virtual System.String Duties { get; set; }
        /// <summary>
        /// 在职状态
        /// </summary>
        public virtual System.String WorkState { get; set; }
        /// <summary>
        /// 工龄
        /// </summary>
        public virtual System.Int32? WorkingYears { get; set; }
        /// <summary>
        /// 员工类型
        /// </summary>
        public virtual System.String EmployeeType { get; set; }
        /// <summary>
        /// 员工类型名称
        /// </summary>
        public virtual System.String EmployeeTypeName { get; set; }
        /// <summary>
        /// 家庭地址
        /// </summary>
        public virtual System.String HomeAddress { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public virtual System.String MobileNO { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual System.Int32? State { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark { get; set; }

        //
        public virtual string LastModifierUserName { get; set; }
        /// <summary>
        /// 在职状态名称
        /// </summary>
        public virtual System.String WorkStateName { get; set; }


        public virtual System.String PostLevel { get; set; }

        public virtual System.String PostLevelName { get; set; }

        public virtual System.String PostProperty { get; set; }
        public virtual System.String PostPropertyName { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public virtual System.String PoliticsName { get; set; }


        public virtual System.String EngageInPost { get; set; }
        public virtual System.String EngageInPostName { get; set; }

        #endregion
    }
}




