using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Infrastructure.FrameworkCore.ToolKit
{
    public class ConvertHelper
    {
        #region 基本数据类型之间的转换

        /**/
        /// <summary>
        /// 判断对象是否为空，如果为空则返回true；反之返回false;
        /// </summary>
        public static bool IsNull(object obj)
        {
            if (obj == null)
            {
                return true;
            }
            return false;
        }

        /**/
        /// <summary>
        /// 将对象转换为string类型，如果对象为空，返回string.Empty
        /// </summary>
        public static string ToString(object obj, string defaultValue = "")
        {
            if (IsNull(obj) || obj.ToString() == string.Empty)
            {
                return defaultValue;
            }
            return obj.ToString();
        }

        /// <summary>
        /// 将对象转换为int类型，异常返回默认值
        /// </summary>
        public static byte ToByte(object obj, byte defaultValue)
        {
            byte temp = defaultValue;
            if (IsNull(obj))
            {
                return defaultValue;
            }
            if (byte.TryParse(obj.ToString(), out defaultValue))
            {
                return defaultValue;
            }
            return temp;
        }

        /**/
        /// <summary>
        /// 将对象转换为int类型，异常返回默认值
        /// </summary>
        public static int ToInt(object obj, int defaultValue = 0)
        {
            int temp = defaultValue;
            if (IsNull(obj))
            {
                return defaultValue;
            }
            if (int.TryParse(obj.ToString(), out defaultValue))
            {
                return defaultValue;
            }
            return temp;
        }


        public static Int64 ToInt64(object obj, Int64 defaultValue = 0)
        {
            Int64 temp = defaultValue;
            if (IsNull(obj))
            {
                return defaultValue;
            }
            if (Int64.TryParse(obj.ToString(), out defaultValue))
            {
                return defaultValue;
            }
            return temp;
        }

        public static long ToLong(object obj, long defaultValue)
        {
            long temp = defaultValue;
            if (IsNull(obj))
            {
                return defaultValue;
            }
            if (long.TryParse(obj.ToString(), out defaultValue))
            {
                return defaultValue;
            }
            return temp;
        }

        /**/
        /// <summary>
        /// 将对象转换为decimal类型，异常返回默认值
        /// </summary>
        public static decimal ToDecimal(object obj, decimal defaultValue = 0)
        {
            decimal temp = defaultValue;
            if (IsNull(obj))
            {
                return defaultValue;
            }
            if (decimal.TryParse(obj.ToString(), out defaultValue))
            {
                return defaultValue;
            }
            return temp;
        }


        /**/
        /// <summary>
        /// 将对象转换为double类型，异常返回默认值
        /// </summary>
        public static double ToDouble(object obj, double defaultValue = 0)
        {
            double temp = defaultValue;
            if (IsNull(obj))
            {
                return defaultValue;
            }
            if (double.TryParse(obj.ToString(), out defaultValue))
            {
                return defaultValue;
            }
            return temp;
        }

        /**/
        /// <summary>
        /// 将对象转换为float类型，异常返回默认值
        /// </summary>
        public static float ToFloat(object obj, float defaultValue)
        {
            float temp = defaultValue;
            if (IsNull(obj))
            {
                return defaultValue;
            }
            if (float.TryParse(obj.ToString(), out defaultValue))
            {
                return defaultValue;
            }
            return temp;
        }

        /**/
        /// <summary>
        /// 将对象转换为DateTime类型，异常返回"1900-01-01"
        /// </summary>
        public static DateTime ToDateTime(object obj)
        {
            DateTime defaultValue = DateTime.MinValue;
            if (IsNull(obj))
            {
                return defaultValue;
            }
            if (DateTime.TryParse(obj.ToString(), out defaultValue))
            {
                return defaultValue;
            }
            return defaultValue;
        }

        /**/
        /// <summary>
        /// 将对象转换为bool类型
        /// </summary>
        public static bool ToBoolean(object Object)
        {
            if (Object == DBNull.Value)
            {
                return false;
            }
            if (!Object.ToString().Equals("1"))
            {
                bool flag = true;
                return Object.ToString().Equals(flag.ToString());
            }
            return true;
        }

        #endregion

        #region
        /// <summary>
        /// 把“,”分割的字符串，转化为List记录集
        /// </summary>
        public static List<int> ToListInt(string source, string separator)
        {
            List<int> listResult = new List<int>();
            char[] arryCharSeparator = separator.ToCharArray();
            string[] arryString = source.Split(arryCharSeparator);
            foreach (string str in arryString)
            {
                listResult.Add(ConvertHelper.ToInt(str, 0));
            }
            return listResult;
        }
        #endregion

        #region 转换人民币大小金额
        /// <summary> 
        /// 转换人民币大小金额 
        /// </summary> 
        /// <param name="num">金额</param> 
        /// <returns>返回大写形式</returns> 
        public static string ToRmb(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }


        /// <summary> 
        /// 转换人民币大小金额  (一个重载，将字符串先转换成数字在调用CmycurD)
        /// </summary> 
        /// <param name="num">用户输入的金额，字符串形式未转成decimal</param> 
        /// <returns></returns> 
        public static string ToRmb(string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return ToRmb(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }
        #endregion

        #region 数据对象转换成字符串，主要用于日志记录

        public static string EntityToString(object t)
        {
            if (t == null)
            {
                return string.Empty;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    builder.Append(string.Format("{0}:{1},", name, value));
                }
                else
                {
                    EntityToString(value);
                }
            }
            return builder.ToString().TrimEnd(',');
        }


        #endregion


        #region 枚举转换

        public static T ConvertEnum<T>(string enumStr)
        {  
            return (T)Enum.Parse(typeof(T), enumStr, false);
           
        }

        #endregion
    }
}
