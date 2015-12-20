using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace  Project.Infrastructure.FrameworkCore.ToolKit.StringHandler
{
    /// <summary>
    /// 涉及到html的处理
    /// </summary>
    public class HtmlHelper
    {
        /// <summary>
        /// 生成指定数量的html空格符号
        /// </summary>
        public static string CreateSpaces(int num)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                sb.Append(" &nbsp;&nbsp;");
            }
            return sb.ToString();
        }


        /// <summary>
        /// 替换回车换行符为html换行符
        /// </summary>
        public static string StrFormat(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\r\n", "<br />");
                str = str.Replace("\n", "<br />");
                str2 = str;
            }
            return str2;
        }


        /// <summary>
        /// 移除Html标记
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }


        #region Html to Text
        /// <summary>
        /// a、先将html文本中的所有空格、换行符去掉（因为html中的空格和换行是被忽略的）
        /// b、将<head>标记中的所有内容去掉
        /// c、将<script>标记中的所有内容去掉
        /// d、将<style>标记中的所有内容去掉
        /// e、将td换成空格，tr,li,br,p 等标记换成换行符
        /// f、去掉所有以“<>”符号为头尾的标记去掉。
        /// g、转换&amp;，&nbps;等转义字符换成相应的符号
        /// h、去掉多余的空格和空行
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlToText(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            string result;
            //remove line breaks,tabs
            result = HttpUtility.HtmlDecode(html);
            result = Regex.Replace(result, @"([\r\n])[\s]+", string.Empty, RegexOptions.IgnoreCase);

            //remove the header
            result = Regex.Replace(result, "(<head>).*(</head>)", string.Empty, RegexOptions.IgnoreCase);

            result = Regex.Replace(result, @"<( )*script([^>])*>", "<script>", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"(<script>).*(</script>)", string.Empty, RegexOptions.IgnoreCase);

            //remove all styles
            result = Regex.Replace(result, @"<( )*style([^>])*>", "<style>", RegexOptions.IgnoreCase); //clearing attributes
            result = Regex.Replace(result, "(<style>).*(</style>)", string.Empty, RegexOptions.IgnoreCase);

            //insert tabs in spaces of <td> tags
            result = Regex.Replace(result, @"<( )*td([^>])*>", " ", RegexOptions.IgnoreCase);

            //insert line breaks in places of <br> and <li> tags
            result = Regex.Replace(result, @"<( )*br( )*>", "\r", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"<( )*li( )*>", "\r", RegexOptions.IgnoreCase);

            //insert line paragraphs in places of <tr> and <p> tags
            result = Regex.Replace(result, @"<( )*tr([^>])*>", "\r\r", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"<( )*p([^>])*>", "\r\r", RegexOptions.IgnoreCase);

            //remove anything thats enclosed inside < >
            result = Regex.Replace(result, @"<[^>]*>", string.Empty, RegexOptions.IgnoreCase);

            //replace special characters:

            result = Regex.Replace(result, @"&#xD;", string.Empty, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&#xA;", string.Empty, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&(reg|#174);", "®", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&nbsp;", " ", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&lt;", "<", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&gt;", ">", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&(.{2,6});", string.Empty, RegexOptions.IgnoreCase);

            //remove extra line breaks and tabs
            result = Regex.Replace(result, @" ( )+", " ");
            result = Regex.Replace(result, "(\r)( )+(\r)", "\r\r");
            result = Regex.Replace(result, @"(\r\r)+", "\r\n");

            return result.Trim();
        }

        public static string NoHTMLExcludeA(string fHtmlString)
        {
            fHtmlString = Regex.Replace(fHtmlString, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

            // 去除 <P></P> 及 </br> 的以外的 HTML
            fHtmlString = Regex.Replace(fHtmlString, @"<(?!a|\/a)[^>]+>", "", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"-->", "", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"<!--.*", "", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            fHtmlString = Regex.Replace(fHtmlString, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            fHtmlString.Replace("<", "");
            fHtmlString.Replace(">", "");
            fHtmlString.Replace("\r\n", "");

            return fHtmlString;
        }

        #endregion

    }
}
