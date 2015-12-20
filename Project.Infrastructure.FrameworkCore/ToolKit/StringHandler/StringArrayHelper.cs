using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Project.Infrastructure.FrameworkCore.ToolKit.StringHandler
{
    /// <summary>
    /// 数组处理
    /// </summary>
    public class StringArrayHelper
    {

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (string.IsNullOrWhiteSpace(strContent))
            {
                return new string[]{};
            }

            if (strContent.IndexOf(strSplit) < 0)
            {
                string[] tmp = { strContent };
                return tmp;
            }
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase).Where(p => !string.IsNullOrWhiteSpace(p)).ToArray(); ;
        }


        /// <summary>
        /// 将字串用指定分割符号,分开,将每个元素转换为INT并用数组返回
        /// </summary>
        /// <param name="str">要转换的字符串如:1,2,3,4</param>
        /// <param name="splite">分隔符如:,</param>
        /// <param name="defaultval">如果有某个元素转换不成功时使用默认值替代</param>
        /// <returns></returns>
        public static int[] StrToIntArray(string str, string splite, int defaultval)
        {
            if (string.IsNullOrEmpty(str)) return null;
            string[] strs = str.Split(new string[] { splite }, StringSplitOptions.None);
            if (strs == null || strs.Length == 0) return null;
            int[] vals = new int[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                vals[i] = ConvertHelper.ToInt(strs[i], defaultval);
            }
            return vals;
        }

        /// <summary>
        /// 将字串用指定分割符号,分开,将每个元素转换为INT并用数组返回,
        /// </summary>
        /// <param name="str">要转换的字符串如:1,2,3,4</param>
        /// <param name="splite">分隔符如:,</param>
        /// <returns></returns>
        public static int[] StrToIntArray(string str, string splite)
        {
            if (string.IsNullOrEmpty(str)) return null;
            string[] strs = str.Split(new string[] { splite }, StringSplitOptions.RemoveEmptyEntries);
            if (strs == null || strs.Length == 0) return null;
            List<int> vals = new List<int>();
            for (int i = 0; i < strs.Length; i++)
            {
                vals.Add(ConvertHelper.ToInt(strs[i], 0));
            }
            return vals.ToArray();
        }

        #region IsIntCollections是否为整数集合
        /// <summary>
        /// 是否为整数集合
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIntCollections(string str)
        {

            bool returnBool = false;
            string[] tempCollections = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in tempCollections)
            {
                returnBool = true;
                try
                {
                    int.Parse(s);
                }
                catch
                {
                    return false;
                }
            }
            return returnBool;
        }
        #endregion

        #region IsStringCollections是否为字符串集合
        /// <summary>
        /// 是否为字符串集合
        /// </summary>
        /// <param name="str"></param>
        /// <param name="spliter">分隔符</param>
        /// <returns></returns>
        public static bool IsStringCollections(string str, params char[] spliter)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("str参数不能为空或null");
            if (spliter == null)
                spliter = new char[] { ',' };
            string[] tempCollections = str.Split(spliter, StringSplitOptions.RemoveEmptyEntries);
            if (tempCollections.Length > 0)
                return true;
            else
                return false;
        }
        #endregion
    }
}
