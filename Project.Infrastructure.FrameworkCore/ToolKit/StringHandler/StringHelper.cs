using System;
using System.Security.Cryptography;
using System.Text;

namespace  Project.Infrastructure.FrameworkCore.ToolKit.StringHandler
{
    public class StringHelper
    {
        #region GetRnd：生成随机字符串
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">目标字符串的长度</param>
        /// <param name="useNum">是否包含数字，1=包含，默认为包含</param>
        /// <param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        /// <param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        /// <param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        /// <param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        /// <returns>指定长度的随机字符串</returns>
        public static string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;

            if (useNum == true) { str += "3456789"; }
            if (useLow == true) { str += "abcdefghjkmnpqrstuvwxy"; }
            if (useUpp == true) { str += "ABCDEFGHJKLMNPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++) { s += str.Substring(r.Next(0, str.Length - 1), 1); }
            return s;
        }
        #endregion

    

        #region GetPhoneFormat 取回电话组合的格式
        /// <summary>
        /// 取回电话组合的格式
        /// </summary>
        /// <param name="phoneCountryKey">电话的国家码</param>
        /// <param name="phoneAreaKey">电话的地址码</param>
        /// <param name="phoneKey">电话号码</param>
        /// <param name="phoneExtKey">分号</param>
        /// <returns>返回组合好的电话格式</returns>
        public static string GetPhoneFormat(string phoneCountryKey, string phoneAreaKey, string phoneKey, string phoneExtKey)
        {
            string strRetu = "";

            if (phoneCountryKey != null && phoneCountryKey != string.Empty)
            {
                strRetu = phoneCountryKey;
            }
            if (phoneAreaKey != null && phoneAreaKey != string.Empty)
            {
                strRetu += "(" + phoneAreaKey + ")";
            }
            if (phoneKey != null && phoneKey != string.Empty)
            {
                strRetu += phoneKey;
            }
            if (phoneExtKey != null && phoneExtKey != string.Empty)
            {
                strRetu += " Ext " + phoneExtKey;
            }
            return strRetu;
        }
        #endregion

        #region CnStringLength：检测字符串中是否含有中文及中文长度
        /// <summary>
        /// 检测字符串中是否含有中文及中文长度
        /// </summary>
        /// <param name="str">要检测的字符串</param>
        /// <returns>中文字符串长度</returns>
        public static int CnStringLength(string str)
        {
            ASCIIEncoding n = new ASCIIEncoding();
            byte[] b = n.GetBytes(str);
            int l = 0;  // l 为字符串之实际长度 
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63)  //判断是否为汉字或全脚符号 
                {
                    l++;
                }
            }
            return l;

        }
        #endregion

    }
}
