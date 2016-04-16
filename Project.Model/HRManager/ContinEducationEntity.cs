

 /***************************************************************************
 *       功能：     HRContinEducation实体类
 *       作者：     Roy
 *       日期：     2016/4/16
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class ContinEducationEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual System.Int32? EmployeeID{get; set;}
        /// <summary>
        /// 用户编号
        /// </summary>
        public virtual System.String EmployeeCode{get; set;}
        /// <summary>
        /// 部门编号
        /// </summary>
        public virtual System.String DepartmentCode{get; set;}
        /// <summary>
        /// 学分类型
        /// </summary>
        public virtual System.String CreditType{get; set;}
        /// <summary>
        /// 学分类型名称
        /// </summary>
        public virtual System.String CreditTypeName{get; set;}
        /// <summary>
        /// 分数
        /// </summary>
        public virtual System.String Score{get; set;}
        /// <summary>
        /// 时间
        /// </summary>
        public virtual System.DateTime? GetTime{get; set;}
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

    
 

