

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
using Project.Model.Other;

namespace Project.Model.HRManager
{
    public class AttendanceEntity : Entity, IHasRemark, IAudited
    {
        #region 属性
        /// <summary>
        /// 通过那次上传 0代表自己补的
        /// </summary>
        public virtual System.Int32? AttendanceUploadRecordId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String EmployeeCode { get; set; }

        public virtual System.String EmployeeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String DepartmentName { get; set; }
        /// <summary>
        /// -1代表缺勤 1代表正常  日班、夜班、公休、值班
        /// </summary>
        public virtual System.String State { get; set; }


        /// <summary>
        /// 考勤日期
        /// </summary>
        public virtual System.DateTime? Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? IsDelete { get; set; }


        public virtual string LastModifierUserCode { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        #endregion


        #region 新增属性
        public virtual DateTime? Attr_StartDate { get; set; }
        public virtual DateTime? Attr_EndDate { get; set; }
        #endregion


        #region 
        public virtual string xh { get; set; }
         public virtual string title { get; set; }
         public virtual string data1 { get; set; }
         public virtual string data2 { get; set; }
         public virtual string data3 { get; set; }
         public virtual string data4 { get; set; }
         public virtual string data5 { get; set; }
         public virtual string data6 { get; set; }
         public virtual string data7 { get; set; }
         public virtual string data8 { get; set; }
         public virtual string data9 { get; set; }
         public virtual string data10 { get; set; }
         public virtual string data11 { get; set; }
         public virtual string data12 { get; set; }
         public virtual string data13 { get; set; }
         public virtual string data14 { get; set; }
         public virtual string data15 { get; set; }
         public virtual string data16 { get; set; }
         public virtual string data17 { get; set; }
         public virtual string data18 { get; set; }
         public virtual string data19 { get; set; }
         public virtual string data20 { get; set; }
         public virtual string data21 { get; set; }
         public virtual string data22 { get; set; }
         public virtual string data23 { get; set; }
         public virtual string data24 { get; set; }
         public virtual string data25 { get; set; }
         public virtual string data26 { get; set; }
         public virtual string data27 { get; set; }
         public virtual string data28 { get; set; }
         public virtual string data29 { get; set; }
         public virtual string data30 { get; set; }
         public virtual string data31 { get; set; }
        #endregion

    }


    [Serializable]
    public class AttendanceResponse
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }
    }
}




