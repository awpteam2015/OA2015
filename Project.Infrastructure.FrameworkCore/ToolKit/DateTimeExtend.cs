using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.ToolKit
{
   public static class DateTimeExtend
    {
       public static string SetDate(this DateTime? tDate, string formatter = "yyyy-MM-dd")
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

        

        /// <summary>
        /// 转DateTime
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str)
        {
            DateTime ret;
            if (!DateTime.TryParse(str, out ret))
                ret = DateTime.MaxValue;
            return ret;
        }

    }
}
