using System.Text.RegularExpressions;

namespace Project.Infrastructure.FrameworkCore.ToolKit.ValidateHandler
{
    /// <summary>
    /// 正则表达式验证
    /// </summary>
    public static class ValidateRegExp
    {
        /// <summary>
        /// 判断是否是昵称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNickName(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return Regex.IsMatch(value.Trim(), @"^[A-Za-z0-9_\-\u4e00-\u9fa5]+$");
        }

        /// <summary>
        /// 判断是否是手机号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsMobile(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return Regex.IsMatch(value.Trim(), @"^(13([0-9])|15([0-9])|18([0-9])|14([0-9])|17([0-9]))\d{8}$");
        }

        /// <summary>
        /// 判断是否是Email
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmail(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return Regex.IsMatch(value.Trim(), @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }

        /// <summary>
        /// 判断是否全是数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFullNumber(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return Regex.IsMatch(value.Trim(), @"^[0-9]*$");
        }

        /// <summary>
        /// 判断是否包含字母
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsLetter(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return Regex.IsMatch(value.Trim(), @"[A-Za-z]");
        }

        /// <summary>
        /// 判断是否包含数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumber(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return Regex.IsMatch(value.Trim(), @"\d");

        }

        /// <summary>
        /// 判断是否包含特殊字符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIllegalChar(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return Regex.IsMatch(value.Trim(), @"[`~!@#%$^&*()=|{}':;',\.<>/?~！￥……&*（）——|{}【】‘；：”“'。，、？+\-_]");
        }

        /// <summary>
        /// 获取密码强度
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string PwdLevel(string value)
        {
            string result;
            var level = 0;
            if (IsNumber(value))
            {
                level++;
            }
            if (IsLetter(value))
            {
                level++;
            }
            if (IsIllegalChar(value))
            {
                level++;
            }
            switch (level)
            {
                case 1:
                    result = "低";
                    break;
                case 2:
                    result = "中";
                    break;
                case 3:
                    result = "高";
                    break;
                default :
                    result = "低";
                    break;
            }
            return result;
        }
    }
}
