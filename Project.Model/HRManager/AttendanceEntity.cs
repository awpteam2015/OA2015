

 /***************************************************************************
 *       功能：     HRAttendance实体类
 *       作者：     李伟伟
 *       日期：     2016/1/23
 *       描述：     人事考勤记录
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.HRManager
{
    public class AttendanceEntity : Entity, IHasRemark, IAudited
    { 
        #region 属性
        /// <summary>
        /// 通过那次上传 0代表自己补的
        /// </summary>
        public virtual System.Int32? AttendanceUploadRecordId{get; set;}
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
        public virtual System.String DepartmentName{get; set;}
        /// <summary>
        /// -1代表缺勤 1代表正常
        /// </summary>
        public virtual System.Int32? State{get; set;}
        /// <summary>
        /// 考勤日期
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
        public virtual System.Int32? IsDelete{get; set;}


        public virtual string LastModifierUserCode { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
		#endregion
        

        #region 新增属性
        
        #endregion



    
    }
}

    
 

