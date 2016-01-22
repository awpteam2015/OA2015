

 /***************************************************************************
 *       功能：     HRYearHholidayDefinition实体类
 *       作者：     Roy
 *       日期：     2016-01-22
 *       描述：     年休存休月定义
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class YearHolidayDefinitionEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 年份
        /// </summary>
        public virtual System.Int32? YearsNum{get; set;}
        /// <summary>
        /// 开始月
        /// </summary>
        public virtual System.Int32? BeginMonth{get; set;}
        /// <summary>
        /// 结束月
        /// </summary>
        public virtual System.Int32? EndMonth{get; set;}
        /// <summary>
        /// 操作人
        /// </summary>
        public virtual System.String CreatorUserCode{get; set;}
        /// <summary>
        /// 操作人姓名
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
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

