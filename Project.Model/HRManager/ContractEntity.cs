

/***************************************************************************
*       功能：     HRContract实体类
*       作者：     李伟伟
*       日期：     2016/1/23
*       描述：     用于记录合同（合同内工资类型等都过滤暂时不考虑）
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using Project.Model.Enum.HRManager;

namespace Project.Model.HRManager
{
    public class ContractEntity : Entity, IHasRemark, IAudited
    {
        #region 属性
        /// <summary>
        /// 工号
        /// </summary>
        public virtual System.String EmployeeCode { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual System.String DepartmentName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual System.DateTime? BeginDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual System.DateTime? EndDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? IsDelete { get; set; }
        /// <summary>
        /// 1 最初签订 2续订 3 变更 4 终止 
        /// </summary>
        public virtual System.Int32? State { get; set; }

        /// <summary>
        /// 是否有效 1有效 2无效
        /// </summary>
        public virtual System.Int32? IsActive { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public virtual System.String ContractNo { get; set; }
        /// <summary>
        /// 甲方
        /// </summary>
        public virtual System.String FirstParty { get; set; }
        /// <summary>
        /// 已方
        /// </summary>
        public virtual System.String SecondParty { get; set; }
        /// <summary>
        /// 合同内容
        /// </summary>
        public virtual System.String ContractContent { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual System.String IdentityCardNo { get; set; }

        /// <summary>
        /// 上级信息
        /// </summary>
        public virtual System.Int16 ParentId { get; set; }

        public virtual System.String FileUrl { get; set; }
        public virtual System.String FileName { get; set; }
        #endregion


        #region 新增属性
        /// <summary>
        /// 1 最初签订 2续订 3 变更 4 终止 
        /// </summary>
        public virtual string Attr_State
        {
            get { return ((ContractStateEnum)State).ToString(); }
        }

        /// <summary>
        /// 是否有效 1有效 2无效
        /// </summary>
        public virtual string Attr_IsActive
        {
            get { return ((ContractIsActiveEnum)IsActive).ToString(); }
        }
        #endregion


    }
}




