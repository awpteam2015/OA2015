

 /***************************************************************************
 *       功能：     HREmployeeRotate实体类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     用于 轮转人员计划
   (轮转人员的设置放入组管理，新建一个组进行管理)
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class EmployeeRotateEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String EmployeeCode{get; set;}
        /// <summary>
        /// 原部门
        /// </summary>
        public virtual System.String DepartmentCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String RotatEDetpCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? BeginDate{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? EenData{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Evaluate{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String EvaluatePersone{get; set;}
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

    
 

