

/***************************************************************************
*       功能：     RMRiverProblemApply实体类
*       作者：     李伟伟
*       日期：     2016/7/24
*       描述：     河道问题申请单
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.RiverManager
{
    public class RiverProblemApplyEntity : Entity, IAudited
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Title { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public virtual System.String Des { get; set; }
        /// <summary>
        /// 问题类型 1日常巡河 2问题上报
        /// </summary>
        public virtual System.Int32 ProblemType { get; set; }
        /// <summary>
        /// 图片地址 多个
        /// </summary>
        public virtual System.String PicUrl { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }

        public virtual System.String DepartmentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? RiverId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String RiverName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserName { get; set; }
        /// <summary>
        /// 坐标
        /// </summary>
        public virtual System.String Coords { get; set; }
        /// <summary>
        /// 问题状态 1.部门待处理 2河长待处理 3完结 4重新申请作废 5回退部门待处理 
        /// </summary>
        public virtual System.Int32? State { get; set; }
        /// <summary>
        /// 部门转发备注
        /// </summary>
        public virtual System.String DepartmentRemark { get; set; }
        /// <summary>
        /// 部门操作时间
        /// </summary>
        public virtual System.DateTime? DepartmentOpTime { get; set; }
        /// <summary>
        /// 顶级部门批注
        /// </summary>
        public virtual System.String TopDepartmentRemark { get; set; }
        /// <summary>
        /// 顶级部门批注时间
        /// </summary>
        public virtual System.DateTime? TopDepartmentOpTime { get; set; }
        /// <summary>
        /// 河长结束问题时间
        /// </summary>
        public virtual System.DateTime? FinishOpTime { get; set; }
        /// <summary>
        /// 河长结束问题备注
        /// </summary>
        public virtual System.String FinishRemark { get; set; }
        /// <summary>
        /// 河长回退问题备注
        /// </summary>
        public virtual System.String ReturnRemark { get; set; }
        /// <summary>
        /// 河长回退问题时间
        /// </summary>
        public virtual System.DateTime? ReturnOpTime { get; set; }
        /// <summary>
        /// 是否曝光
        /// </summary>
        public virtual System.Int32? IsExposure { get; set; }
        /// <summary>
        /// 曝光等级
        /// </summary>
        public virtual System.Int32? ExposureLever { get; set; }
        /// <summary>
        /// 是否已发送短信
        /// </summary>
        public virtual System.Int32? IsSendMessage { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual System.Int32? IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CreatorUserName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String LastModifierUserName { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark { get; set; }
        /// <summary>
        /// 删除原因
        /// </summary>
        public virtual System.String DeleteRemark { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual System.Int32? IsDeleted { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        public virtual System.String DeleteUserName { get; set; }
        /// <summary>
        /// 删除人编码
        /// </summary>
        public virtual System.String DeleteUserCode { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual System.DateTime? DeleteTime { get; set; }


        public virtual System.Int32 IsUrgent { get; set; }

        public virtual System.String UrgentRemark { get; set; }
        #endregion


        #region 新增属性
        public virtual string Attr_ProblemTypeStr
        {
            get
            {
                switch (ProblemType)
                {
                    case 1:
                        return "日常巡河";
                    case 2:
                        return "问题上报";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 1.部门待处理 2河长待处理 3完结 4重新申请作废 5回退部门待处理 
        /// </summary>
        public virtual string Attr_StateStr
        {
            get
            {
                switch (State)
                {
                    case 1:
                        return "部门待处理";
                    case 2:
                        return "河长待处理";
                    case 3:
                        return "完结";
                    case 4:
                        return "重新申请作废";
                    case 5:
                        return "回退部门待处理";
                    default:
                        return "";
                }

            }
        }

        public virtual string Attr_IsUrgent
        {
            get
            {
                switch (IsUrgent)
                {
                    case 1:
                        return "是";
                    default:
                        return "否";
                }

            }
        }

        public virtual string Attr_IsExposure
        {
            get
            {
                switch (IsExposure)
                {
                    case 1:
                        return "是";
                    default:
                        return "否";
                }

            }
        }


        public virtual string Attr_IsSendMessage
        {
            get
            {
                switch (IsSendMessage)
                {
                    case 1:
                        return "是";
                    default:
                        return "否";
                }

            }
        }



        public virtual string Attr_ExpireStr
        {
            get
            {
                switch (State)
                {
                    case 1:
                    case 2:
                    case 5:
                        if (DateTime.Now.Subtract(CreationTime.GetValueOrDefault()).Days > 7)
                        {
                            return "<span style=\"color:red\">过期未处理</span>";
                        }
                        return "";
                    default:
                        return "";
                }

            }
        }


        public virtual System.DateTime? Attr_CreationTimeStart { get; set; }

        public virtual System.DateTime? Attr_CreationTimeEnd { get; set; }
        #endregion
    }
}




