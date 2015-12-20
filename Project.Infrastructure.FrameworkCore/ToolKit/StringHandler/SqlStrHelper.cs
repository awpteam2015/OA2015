using System.Text.RegularExpressions;

namespace  Project.Infrastructure.FrameworkCore.ToolKit.StringHandler
{
    /// <summary>
    /// 对sql语句的有危险的字符的处理
    /// </summary>
    public class SqlStrHelper
    {
        /// <summary>
        /// 移除字符串中的可能引起危险Sql字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSqlUnsafeString(string str)
        {
            string p = @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']";
            return Regex.Replace(str, p, "");
        }

        /// <summary>
        /// 移除字符串中的可能引起危险Sql字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSqlUnsafeString2(string str)
        {
            string p = @"[;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']";
            return Regex.Replace(str, p, "");
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 替换sql语句中的有问题符号
        /// </summary>
        public static string ChkSQL(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("'", "''").Replace("0x", "0-x");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        /// 改正sql语句中的转义字符
        /// </summary>
        public static string MashSQL(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\'", "'");
                str2 = str;
            }
            return str2;
        }
    }
}
