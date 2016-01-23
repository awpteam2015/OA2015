﻿

 /***************************************************************************
 *       功能：     HRContract实体类
 *       作者：     李伟伟
 *       日期：     2016/1/23
 *       描述：     用于记录合同（合同内工资类型等都过滤暂时不考虑）
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class ContractEntity: Entity
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
        /// 
        /// </summary>
        public virtual System.DateTime? BeginDate{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? EndDate{get; set;}
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
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? IsDelete{get; set;}
        /// <summary>
        /// 1 最初签订 2续订 3 变更 4 终止 
        /// </summary>
        public virtual System.Int32? State{get; set;}
        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual System.Int32? IsActive{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

