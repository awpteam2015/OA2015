

 /***************************************************************************
 *       功能：     HRGroupEmployee实体类
 *       作者：     Roy
 *       日期：     2016-01-16
 *       描述：     组成员管理 
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class GroupEmployeeEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 组编号
        /// </summary>
        public virtual System.String GroupCode{get; set;}
        /// <summary>
        /// 员工编号
        /// </summary>
        public virtual System.String EmployeeCode{get; set;}
        /// <summary>
        /// 操作员
        /// </summary>
        public virtual System.String CreatorUserCode{get; set;}
        /// <summary>
        /// 操作员
        /// </summary>
        public virtual System.String CreatorUserName{get; set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreateTime{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

