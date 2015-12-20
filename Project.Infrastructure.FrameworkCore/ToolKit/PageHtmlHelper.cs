using System.Text.RegularExpressions;

namespace Project.Infrastructure.FrameworkCore.ToolKit
{
   public class PageHtmlHelper
    {

        /// <summary>
        /// 删除HTML标记
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string RemoveHtml(string Htmlstring) 
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"(?s)<script.*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除样式
            Htmlstring = Regex.Replace(Htmlstring, @"(?s)<style.*?>.*?</style>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring = Htmlstring.Replace("<", "〈");
            Htmlstring = Htmlstring.Replace(">", "〉");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = Htmlstring = Htmlstring.Replace("&nbsp;", "");
            Htmlstring = Htmlstring.Replace("&ldquo;", "“");
            Htmlstring = Htmlstring.Replace("&rdquo;", "”");
            Htmlstring = Htmlstring.Replace("&amp;", "&");
            Htmlstring = System.Web.HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }


    }
}
