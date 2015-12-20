/*******************************************************
版权所有：
用    途：时间处理类

结构组成：

说    明：

作    者：李伟伟

创建日期：2009-02-16
历史记录：

*****************************************************
修改人员：               修改日期： 
修改说明：   
*******************************************************/

using System;
using System.Collections.Generic;

namespace Project.Infrastructure.FrameworkCore.ToolKit
{
    /// <summary>
    /// 日期时间处理
    /// </summary>
    public class DateTimeHelper
    {
        public enum DateInterval
        {
            Second, Minute, Hour, Day, Week, Month, Quarter, Year
        }

        public static string SetDate(DateTime? tDate, string formatter="yyyy-MM-dd HH:mm")
        {
            if (tDate == null)
            {
                return "";
            }
            else
            {
                return tDate.GetValueOrDefault().ToString(formatter);
            }
        }

        #region Quarter:取得某个日期是本年度的第几个季度
        /// <summary> 
        /// 取得某个日期是本年度的第几个季度. 
        /// </summary> 
        /// <param name="tDate"></param> 
        /// <returns></returns> 
        public static int Quarter(DateTime tDate)
        {
            switch (tDate.Month)
            {
                case 1:
                case 2:
                case 3:
                    return 1;

                case 4:
                case 5:
                case 6:
                    return 2;

                case 7:
                case 8:
                case 9:
                    return 3;

                default:
                    return 4;
            }

        }
        #endregion

        #region DateDiff:实现与SQL和VB中 DateDiff函数的功能
        /// <summary> 
        /// 实现与SQL和VB中 DateDiff函数的功能 
        /// 返回一个 Long 值，用于指定两个 Date 值之间的时间间隔数。 
        /// </summary> 
        /// <param name="Interval"></param> 
        /// <param name="StartDate"></param> 
        /// <param name="EndDate"></param> 
        /// <returns></returns> 
        public static long DateDiff(DateInterval Interval, System.DateTime StartDate, System.DateTime EndDate)
        {
            long lngDateDiffValue = 0;
            System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (Interval)
            {
                case DateInterval.Second:
                    lngDateDiffValue = (long)TS.TotalSeconds;
                    break;
                case DateInterval.Minute:
                    lngDateDiffValue = (long)TS.TotalMinutes;
                    break;
                case DateInterval.Hour:
                    lngDateDiffValue = (long)TS.TotalHours;
                    break;
                case DateInterval.Day:
                    lngDateDiffValue = (long)TS.Days;
                    break;
                case DateInterval.Week:
                    //这里应该注意余数的问题
                    lngDateDiffValue = (TS.Days % 7 > 0) ? (long)(TS.Days / 7) + 1 : (long)(TS.Days / 7);
                    break;
                case DateInterval.Month:
                    //应取两个时间的月份之差(季度和年同理) 
                    lngDateDiffValue = (long)(EndDate.Year - StartDate.Year) * 12 + (EndDate.Month - StartDate.Month);
                    break;
                case DateInterval.Quarter:
                    lngDateDiffValue = (long)(EndDate.Year - StartDate.Year) * 4 + (Quarter(EndDate) - Quarter(StartDate));
                    break;
                case DateInterval.Year:
                    lngDateDiffValue = (long)(EndDate.Year - StartDate.Year);
                    break;
            }
            return (lngDateDiffValue);
        }
        #endregion

        #region GetDiffDays:计算差异天数
        /// <summary>
        /// 计算差异天数
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>相差的天数</returns>
        public static int GetDiffDays(string startDate, string endDate)
        {
            DateTime dtStart = DateTime.ParseExact(startDate, "yyyy/MM/dd", null);
            DateTime dtEnd = DateTime.ParseExact(endDate, "yyyy/MM/dd", null);
            // 計算差異天數
            TimeSpan tsDay = dtEnd - dtStart;
            int dayCount = (int)tsDay.TotalDays;
            return dayCount;
        }
        #endregion

        #region GetTheHoursOfDay:取得某日期的 24 小時時刻列表
        /// <summary>
        /// 取得某日期的 24 小時時刻列表
        /// </summary>
        /// <param name="dt">某日期</param>
        /// <returns>某日期的 24 小時時刻列表</returns>
        public static DateTime[] GetTheHoursOfDay(DateTime dt)
        {
            List<DateTime> dtList = new List<DateTime>();

            for (int i = 0; i < 24; i++)
            {
                dtList.Add(new DateTime(dt.Year, dt.Month, dt.Day, i, 0, 0));
            }

            return dtList.ToArray();
        }
        #endregion

        #region GetTheFirstDayOfWeek:取得某日期在該星期的第一天 (星期日)
        /// <summary>
        /// 取得某日期在該星期的第一天 (星期日)
        /// </summary>
        /// <param name="dt">某日期</param>
        /// <returns>某日期在該星期的第一天 (星期日)</returns>
        public static DateTime GetTheFirstDayOfWeek(DateTime dt)
        {
            return dt.AddDays((int)dt.DayOfWeek * -1).Date;
        }
        #endregion

        #region GetTheLastDayOfWeek:取得某日期在該星期的最後一天 (星期六)
        /// <summary>
        /// 取得某日期在該星期的最後一天 (星期六)
        /// </summary>
        /// <param name="dt">某日期</param>
        /// <returns>某日期在該星期的最後一天 (星期六)</returns>
        public static DateTime GetTheLastDayOfWeek(DateTime dt)
        {
            return dt.AddDays(7 + (int)dt.DayOfWeek * -1 - 1).Date;
        }
        #endregion

        #region GetTheFirstDayOfMonth:取得某日期在該月份的第一天 (1 號)
        /// <summary>
        /// 取得某日期在該月份的第一天 (1 號)
        /// </summary>
        /// <param name="dt">某日期</param>
        /// <returns>某日期在該月份的第一天</returns>
        public static DateTime GetTheFirstDayOfMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        #endregion

        #region GetTheLastDayOfMonth:取得某日期在該月份的最後一天
        /// <summary>
        /// 取得某日期在該月份的最後一天
        /// </summary>
        /// <param name="dt">某日期</param>
        /// <returns>某日期在該月份的最後一天</returns>
        public static DateTime GetTheLastDayOfMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month + 1, 1).AddDays(-1);
        }
        #endregion

        #region GetTheFirstDaysOfWeekInMoth:取得某日期在該月份每周的第一天列表
        /// <summary>
        /// 取得某日期在該月份每周的第一天列表
        /// </summary>
        /// <param name="dt">某日期</param>
        /// <returns>某日期在該月份每周的第一天列表</returns>
        public static DateTime[] GetTheFirstDaysOfWeekInMoth(DateTime dt)
        {
            List<DateTime> dtList = new List<DateTime>();

            DateTime dtTemp = GetTheFirstDayOfWeek(GetTheFirstDayOfMonth(dt)).Date;
            DateTime dtEnd = GetTheLastDayOfMonth(dt).Date;

            for (int i = 0; i < 6; i++)
            {
                if (dtTemp.AddDays(i * 7) <= dtEnd)
                {
                    dtList.Add(dtTemp.AddDays(i * 7));
                }
            }

            return dtList.ToArray();
        }
        #endregion

        #region GetTheFirstDayOfQuarter:取得某日期在該季的第一天
        /// <summary>
        /// 取得某日期在該季的第一天
        /// </summary>
        /// <param name="dt">某日期</param>
        /// <returns>某日期在該季的第一天</returns>
        public static DateTime GetTheFirstDayOfQuarter(DateTime dt)
        {
            if (dt >= new DateTime(dt.Year, 1, 1) && dt <= new DateTime(dt.Year, 3, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59))
            {
                return new DateTime(dt.Year, 1, 1);
            }
            else if (dt >= new DateTime(dt.Year, 4, 1) && dt <= new DateTime(dt.Year, 6, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59))
            {
                return new DateTime(dt.Year, 4, 1);
            }
            else if (dt >= new DateTime(dt.Year, 7, 1) && dt <= new DateTime(dt.Year, 9, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59))
            {
                return new DateTime(dt.Year, 7, 1);
            }
            else
            {
                return new DateTime(dt.Year, 10, 1);
            }
        }
        #endregion

        #region GetTheLastDayOfQuarter:取得某日期在該季的最後一天
        /// <summary>
        /// 取得某日期在該季的最後一天
        /// </summary>
        /// <param name="dt">某日期</param>
        /// <returns>某日期在該季的最後一天</returns>
        public static DateTime GetTheLastDayOfQuarter(DateTime dt)
        {
            if (dt >= new DateTime(dt.Year, 1, 1) && dt <= new DateTime(dt.Year, 3, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59))
            {
                return new DateTime(dt.Year, 3, DateTime.DaysInMonth(dt.Year, dt.Month));
            }
            else if (dt >= new DateTime(dt.Year, 4, 1) && dt <= new DateTime(dt.Year, 6, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59))
            {
                return new DateTime(dt.Year, 6, DateTime.DaysInMonth(dt.Year, dt.Month));
            }
            else if (dt >= new DateTime(dt.Year, 7, 1) && dt <= new DateTime(dt.Year, 9, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59))
            {
                return new DateTime(dt.Year, 9, DateTime.DaysInMonth(dt.Year, dt.Month));
            }
            else
            {
                return new DateTime(dt.Year, 12, DateTime.DaysInMonth(dt.Year, dt.Month));
            }
        }
        #endregion

        #region GetTheFirstDaysOfMonthInQuarter:取得某日期在該季每個月的第一天列表
        /// <summary>
        /// 取得某日期在該季每個月的第一天列表
        /// </summary>
        /// <param name="dt">某日期</param>
        /// <returns>取得某日期在該季每個月的第一天列表</returns>
        public static DateTime[] GetTheFirstDaysOfMonthInQuarter(DateTime dt)
        {
            List<DateTime> dtList = new List<DateTime>();

            DateTime dtTemp = GetTheFirstDayOfQuarter(dt);
            DateTime dtEnd = GetTheLastDayOfQuarter(dt);

            for (int i = 0; i < 3; i++)
            {
                if (new DateTime(dt.Year, dtTemp.AddMonths(i).Month, 1) <= dtEnd)
                {
                    dtList.Add(new DateTime(dt.Year, dtTemp.AddMonths(i).Month, 1));
                }
            }

            return dtList.ToArray();
        }
        #endregion

        #region GetTheFirstDayOfYear:取得某日期在當年的第一天
        /// <summary>
        /// 取得某日期在當年的第一天
        /// </summary>
        /// <param name="dt">某日期</param>
        /// <returns>某日期在當年的第一天</returns>
        public static DateTime GetTheFirstDayOfYear(DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1);
        }
        #endregion

        #region GetTheLastDayOfYear:取得某日期在當年的最後一天
        /// <summary>
        /// 取得某日期在當年的最後一天
        /// </summary>
        /// <param name="dt">某日期</param>
        /// <returns>某日期在當年的最後一天</returns>
        public static DateTime GetTheLastDayOfYear(DateTime dt)
        {
            return new DateTime(dt.Year, 12, DateTime.DaysInMonth(dt.Year, 12));
        }
        #endregion

        #region GetTheFirstDaysOfQuarterInYear:取得某日期於當年每一季的第一天列表
        /// <summary>
        /// 取得某日期於當年每一季的第一天列表
        /// </summary>
        /// <param name="dt">某日期</param>
        /// <returns>某日期於當年每一季的第一天列表</returns>
        public static DateTime[] GetTheFirstDaysOfQuarterInYear(DateTime dt)
        {
            List<DateTime> dtList = new List<DateTime>();
            dtList.Add(new DateTime(dt.Year, 1, 1));
            dtList.Add(new DateTime(dt.Year, 4, 1));
            dtList.Add(new DateTime(dt.Year, 7, 1));
            dtList.Add(new DateTime(dt.Year, 10, 1));

            return dtList.ToArray();
        }
        #endregion

    }
}
