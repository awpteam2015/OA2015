using System.Text.RegularExpressions;

namespace  Project.Infrastructure.FrameworkCore.ToolKit.ValidateHandler
{
    public class Regexdefine
    {
        /// <summary>
        /// 匹配任何空白字符，包括空格、制表符、换页符等等。等价于[\f\n\r\t\v]。
        /// </summary>
        public static readonly Regex WhitespaceRegex = new Regex("\\s+", RegexOptions.Compiled);
        /// <summary>
        /// 匹配中文字符
        /// </summary>
        public static readonly Regex ChineseRegex = new Regex("[\u4e00-\u9fa5]", RegexOptions.Compiled);
    }
}
