﻿

 /***************************************************************************
 *       功能：     HREmployeeInfoHis实体类
 *       作者：     Roy
 *       日期：     2016-02-16
 *       描述：     通过FSate字段进行过滤是还是历史记录
   人员基础信息，如需要增加多字段请使用扩展表
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class EmployeeInfoHisEntity: Entity, ISoftDelete
    { 
        #region 属性
        /// <summary>
        /// 员工表ID
        /// </summary>
        public virtual System.Int32? EmployeeID{get; set;}
        /// <summary>
        /// 员工编号
        /// </summary>
        public virtual System.String EmployeeCode{get; set;}
        /// <summary>
        /// 员工名称
        /// </summary>
        public virtual System.String EmployeeName{get; set;}
        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual System.String DepartmentCode{get; set; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        public virtual System.String DepartmentName { get; set; }

        /// <summary>
        /// 修改后部门
        /// </summary>
        public virtual System.String InDepartmentCode { get; set; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        public virtual System.String InDepartmentName { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public virtual System.String JobName{get; set;}
        /// <summary>
        /// 中文简拼
        /// </summary>
        public virtual System.String PayCode{get; set;}
        /// <summary>
        /// 姓别
        /// </summary>
        public virtual System.Int32? Sex{get; set;}
        /// <summary>
        /// 身份证
        /// </summary>
        public virtual System.String CertNo{get; set;}
        /// <summary>
        /// 生日
        /// </summary>
        public virtual System.DateTime? Birthday{get; set;}
        /// <summary>
        /// 技术职称名称
        /// </summary>
        public virtual System.String TechnicalTitleName{get; set;}
        /// <summary>
        /// 技术职称
        /// </summary>
        public virtual System.String TechnicalTitle{get; set;}
        /// <summary>
        /// 职务名称
        /// </summary>
        public virtual System.String DutiesName{get; set;}
        /// <summary>
        /// 单位职务
        /// </summary>
        public virtual System.String Duties{get; set;}
        /// <summary>
        /// 工龄
        /// </summary>
        public virtual System.Int32? WorkingYears{get; set;}
        /// <summary>
        /// 在职状态
        /// </summary>
        public virtual System.String WorkState{get; set;}

        /// <summary>
        /// 修改后状态
        /// </summary>
        public virtual System.String InWorkState { get; set; }
        /// <summary>
        /// 员工类型
        /// </summary>
        public virtual System.String EmployeeType{get; set;}
        /// <summary>
        /// 员工类型名称
        /// </summary>
        public virtual System.String EmployeeTypeName{get; set;}
        /// <summary>
        /// 家庭地址
        /// </summary>
        public virtual System.String HomeAddress{get; set;}
        /// <summary>
        /// 手机号
        /// </summary>
        public virtual System.String MobileNO{get; set;}
        /// <summary>
        /// 图片地址
        /// </summary>
        public virtual System.String ImageUrl{get; set;}
        /// <summary>
        /// 排序
        /// </summary>
        public virtual System.Int32? Sort{get; set;}
        /// <summary>
        /// 状态
        /// </summary>
        public virtual System.Int32? State{get; set;}
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark{get; set;}
        /// <summary>
        /// 操作员
        /// </summary>
        public virtual System.String CreatorUserCode{get; set;}
        /// <summary>
        /// 操作员名称
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
        /// <summary>
        /// 在职状态名称
        /// </summary>
        public virtual System.String WorkStateName{get; set; }


        /// 修改后状态
        /// </summary>
        public virtual System.String InWorkStateName { get; set; }
        public virtual bool IsDeleted { get; set; }
        /// <summary>
        /// 是否录入
        /// </summary>
        public virtual int? IsInsert { get; set; }


        public virtual System.String PostLevel { get; set; }

        public virtual System.String PostLevelName { get; set; }

        public virtual System.String PostProperty { get; set; }
        public virtual System.String PostPropertyName { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public virtual System.String PoliticsName { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}

    
 

