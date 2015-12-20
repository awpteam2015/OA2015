using System.Text.RegularExpressions;

namespace Project.Infrastructure.FrameworkCore.ToolKit
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// 判断字符串是不是EMAIL
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        /// <summary>
        /// 判断字符串是不是URL
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsUrlAddress(this string s)
        {
            Regex regex = new Regex(@"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$");
            return regex.IsMatch(s);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public static bool IsNumber(this string s)
        {
            Regex regex = new Regex(@"^[\d]?$");
            return regex.IsMatch(s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReplaceScript(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            s = Regex.Replace(s, "[\\s]{2,}", " ");   //two or more spaces

            s = Regex.Replace(s, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");   //<br>

            s = Regex.Replace(s, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");   //&nbsp;

            s = Regex.Replace(s, "<(.|\\n)*?>", string.Empty);  //any other tags

            s = s.Replace("'", "''");
            return s;

        }

    }
}
