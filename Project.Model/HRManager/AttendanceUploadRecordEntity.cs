

 /***************************************************************************
 *       功能：     HRAttendanceUploadRecord实体类
 *       作者：     李伟伟
 *       日期：     2016/1/23
 *       描述：     人事考勤上传记录
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class AttendanceUploadRecordEntity : Entity, IHasRemark
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String DepartmentCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? Date{get; set;}
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
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String FileUrl{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? IsDelete{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

