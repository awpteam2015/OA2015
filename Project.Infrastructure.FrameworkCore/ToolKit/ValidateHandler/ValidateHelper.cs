using System;
using System.Linq;
using System.Text.RegularExpressions;
using Project.Infrastructure.FrameworkCore.ToolKit.StringHandler;

namespace  Project.Infrastructure.FrameworkCore.ToolKit.ValidateHandler
{
    /// <summary>
    /// 验证
    /// </summary>
    public static class ValidateHelper
    {
        #region 正则表达式验证
        //RegexOptions.Compiled ：运行期编译为代码，提高执行速度
        private static readonly Regex EmailRegex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled);
        //@"^http:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\""\""])*$"
        private static readonly Regex UrlRegex = new Regex(@"^(http|HTTP)://([\w-]+\.)*[\w-]+(:\d+)?(/[\u4e00-\u9fa5\w- ./?%&=]*)?$", RegexOptions.Compiled);
        private static readonly Regex UserNameRegex = new Regex(@"^[a-zA-Z0-9_\u4e00-\u9fa5]{3,20}$", RegexOptions.Compiled);
        private static readonly Regex PasswordRegex = new Regex(@"^[a-zA-Z]\w{5,17}$", RegexOptions.Compiled);
        private static readonly Regex ZipRegex = new Regex(@"^[1-9]\d{5}$", RegexOptions.Compiled);
        private static readonly Regex TelRegex = new Regex(@"^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$", RegexOptions.Compiled);
        private static readonly Regex MobileRegex = new Regex(@"^[1-9]\d{5}$", RegexOptions.Compiled);
        private static readonly Regex PhoneRegex = new Regex(@"^[1-9]\d{5}$", RegexOptions.Compiled);


        private static readonly string[] QuartzCronExpressionSpecialCharacter = { ",", "-", "/" };
        private static readonly string[] QuartzCronExpressionSpecialCharacterDay = { "*", "?", "L", "W", "C" };
        private static readonly string[] QuartzCronExpressionSpecialCharacterWeek = { "*", "?", "L", "C" }; //, "#"


        /// <summary>
        /// 判断一个字符串是否是正整数 数字
        /// </summary>
        /// <param name="_string"></param>
        /// <returns></returns>
        public static bool IsDigit(this string _string)
        {
            if (string.IsNullOrEmpty(_string))
                return false;
            foreach (char c in _string)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 验证Email地址
        /// </summary>
        public static bool IsValidEmail(this string strIn)
        {
            return EmailRegex.IsMatch(strIn);
        }

        /// <summary>
        /// 验证Url地址 
        /// </summary>
        public static bool IsValidUrl(this string strIn)
        {
            return UrlRegex.IsMatch(strIn);
        }

        /// <summary>
        /// 验证用户名
        /// </summary>
        public static bool IsValidUserName(this string strIn)
        {
            return UserNameRegex.IsMatch(strIn);
        }

        /// <summary>
        /// 验证用户密码  
        /// 正确格式为：以字母开头，长度在6~18之间，只能包含字符、数字和下划线。
        /// </summary>
        public static bool IsValidPassword(this string strIn)
        {
            return PasswordRegex.IsMatch(strIn);
        }

        /// <summary>
        /// 验证邮政编码
        /// </summary>
        public static bool IsValidZip(this string strIn)
        {
            return ZipRegex.IsMatch(strIn);
        }

        /// <summary>
        /// 验证固定电话，可以是以 ；号分割的多个电话号码 
        /// </summary>
        public static bool IsValidTel(this string strIn)
        {
            string[] arrayTel = strIn.Split('；');
            int iTelCount = arrayTel.Length;
            for (int i = 0; i < iTelCount; i++)
            {
                if (!TelRegex.IsMatch(arrayTel[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 手机号码，可以是以 ；号分割的多个电话号码 
        /// </summary>
        public static bool IsValidMobile(this string strIn)
        {
            return MobileRegex.IsMatch(strIn);
        }

        /// <summary>
        /// 验证电话号码（固定电话或电机），可以是以 ；号分割的多个电话号码 
        /// </summary>
        public static bool IsValidPhone(this string strIn)
        {
            string[] arrayPhone = strIn.Split('；');
            int iPhoneCount = arrayPhone.Length;
            for (int i = 0; i < iPhoneCount; i++)
            {
                if (!PhoneRegex.IsMatch(arrayPhone[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 验证是否为中文字符
        /// </summary>
        public static bool IsValidChinese(this string strIn)
        {
            return Regexdefine.ChineseRegex.IsMatch(strIn);
        }





        //验证是否为小数 
        public static bool IsValidDecimal(this string strIn)
        {
            return Regex.IsMatch(strIn, @"[0].\d{1,2}|[1]");
        }
        //验证年月日 
        public static bool IsValidDate(this string strIn)
        {
            return Regex.IsMatch(strIn, @"^2\d{3}-(?:0?[1-9]|1[0-2])-(?:0?[1-9]|[1-2]\d|3[0-1])(?:0?[1-9]|1\d|2[0-3]):(?:0?[1-9]|[1-5]\d):(?:0?[1-9]|[1-5]\d)$");
        }
        //验证域名
        public static bool IsValidDomain(this string strIn)
        {
            return Regex.IsMatch(strIn, @"(?=^.{1,254}$)(^(?:(?!\d+\.)[a-zA-Z0-9_\-]{1,63}\.?)+(?:[a-zA-Z]{2,})$)");
        }
        /// <summary>
        /// 验证后缀名
        /// bool b = "12345.txt".IsValidPostfix(@"gif|jpg");
        /// </summary>
        /// <param name="pattern">gif|jpg</param>
        /// <returns></returns>
        public static bool IsValidFilePostfix(this string strIn, string pattern)
        {
            return Regex.IsMatch(strIn, @"\.(?i:" + pattern + ")$");
        }

        /// <summary>
        /// 判断字符串是否为符合正则表达式要求
        /// bool b = "12345".IsMatch(@"\d+");
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static bool IsMatch(this string s, string pattern)
        {
            if (s == null) return false;
            else return Regex.IsMatch(s, pattern);
        }
        #endregion

        #region Quartz Cron Expression规则验证
        /// <summary>
        /// Quartz Cron Expression规则验证
        /// 特殊符号：",", "-", "*", "/", "?","#"；"*"和"?" 可以单独使用，其它是链接符如：0/10
        /// 字符："L", "W", "C"；
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsValidQuartzCronExpression(this string expression)
        {
            bool result = false;
            string[] arryQuartzCronExpression = expression.Trim().Split(' ');
            if (arryQuartzCronExpression.Count() == 6)
            {
                //? 号只能用在日和周域上，但是不能在这两个域上同时使用
                if (arryQuartzCronExpression[3] == "?" && arryQuartzCronExpression[5] == "?")
                {
                    return false;
                }

                int i = 1;
                foreach (string expressionChar in arryQuartzCronExpression)
                {
                    int expressionValue = ConvertHelper.ToInt(expressionChar, -1);
                    if (expressionValue == -1)
                    {
                        switch (i)
                        {
                            case 4:
                                if (expressionChar.In(QuartzCronExpressionSpecialCharacterDay))
                                    result = true;
                                else
                                    return false;
                                break;
                            case 6:
                                if (expressionChar.In(QuartzCronExpressionSpecialCharacterWeek))
                                    result = true;
                                else
                                {
                                    result = IsSpecialCharRule(i, expressionChar);
                                }

                                if (result == false)
                                    return false;
                                break;
                            default:
                                if (expressionChar == "*")
                                    result = true;
                                else
                                {
                                    result = IsSpecialCharRule(i, expressionChar);
                                }

                                if (result == false)
                                    return false;
                                break;
                        }
                    }
                    else
                    {
                        result = IsDigitRule(i, expressionValue);

                    }
                    i++;
                }

                return result;
            }
            else
            {
                return false;
            }
        }

        private static bool IsDigitRule(int position, int expressionValue)
        {
            bool result = false;
            switch (position)
            {
                case 1: //秒
                case 2: //分
                    if (expressionValue.InRange(0, 59))
                        result = true;
                    else
                        return false;
                    break;
                case 3: //时
                    if (expressionValue.InRange(0, 23))
                        result = true;
                    else
                        return false;
                    break;
                case 4: //日
                    if (expressionValue.InRange(1, 31))
                        result = true;
                    else
                        return false;
                    break;
                case 5: //月
                    if (expressionValue.InRange(1, 12))
                        result = true;
                    else
                        return false;
                    break;
                case 6: //周
                    if (expressionValue.InRange(1, 7))
                        result = true;
                    else
                        return false;
                    break;
            }
            return result;
        }

        private static bool IsSpecialCharRule(int position, string expressionChar)
        {
            bool result = false;

            foreach (string specialChar in QuartzCronExpressionSpecialCharacter)
            {
                if (expressionChar.IndexOf(specialChar) > 0)
                {
                    string[] arryCombinationExpressionChar = expressionChar.Split(Convert.ToChar(specialChar));
                    if (specialChar == ",")
                    {
                        foreach (string iChar in arryCombinationExpressionChar)
                        {
                            result = IsDigitRule(position, ConvertHelper.ToInt(iChar, -1));
                        }
                    }
                    else
                    {
                        if (arryCombinationExpressionChar.Count() == 2)
                        {
                            foreach (string iChar in arryCombinationExpressionChar)
                            {
                                result = IsDigitRule(position, ConvertHelper.ToInt(iChar, -1));
                            }
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    break;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }
        #endregion

        #region 验证身份证号
        /// <summary>
        /// 验证身份证号
        /// </summary>
        /// <param name="Id">待验证好</param>
        /// <returns>是否合法</returns>
        public static bool CheckIDCard(string Id)
        {
            int intLen = Id.Length;
            long n = 0;

            if (intLen == 18)
            {
                if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
                {
                    return false;//数字验证
                }
                string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                if (address.IndexOf(Id.Remove(2)) == -1)
                {
                    return false;//省份验证
                }
                string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证
                }
                string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                char[] Ai = Id.Remove(17).ToCharArray();
                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
                }
                int y = -1;
                Math.DivRem(sum, 11, out y);
                if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
                {
                    return false;//校验码验证
                }
                return true;//符合GB11643-1999标准
            }
            else if (intLen == 15)
            {
                if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
                {
                    return false;//数字验证
                }
                string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                if (address.IndexOf(Id.Remove(2)) == -1)
                {
                    return false;//省份验证
                }
                string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证
                }
                return true;//符合15位身份证标准
            }
            else
            {
                return false;//位数不对
            }
        }
        #endregion

    }
}
