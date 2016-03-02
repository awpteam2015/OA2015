

/***************************************************************************
*       功能：     SysHolidayDetail实体类
*       作者：     Roy
*       日期：     2016-01-20
*       描述：     节假日管理
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.SystemSetManager
{
    public class HolidayDetailEntity : Entity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String HolidayName { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public virtual System.DateTime? HolidayDate { get; set; }

        /// <summary>
        /// 假期类型 0 公休（周未） 1 法定节假日
        /// </summary>
        public virtual System.Int32? HolidayDateType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public virtual System.String CreatorUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        #endregion


        #region 新增属性

        /// <summary>
        /// 结束日期 用于查询
        /// </summary>
        public virtual System.DateTime? HolidayDateEnd { get; set; }


        #endregion
    }
}




