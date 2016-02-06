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
    }
}
