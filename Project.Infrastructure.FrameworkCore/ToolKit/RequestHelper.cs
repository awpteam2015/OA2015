using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;

namespace Project.Infrastructure.FrameworkCore.ToolKit
{
    /// <summary>
    /// Request.QueryString  Request.Form
    /// </summary>
    public class RequestHelper
    {

        #region 属性

        /// <summary>
        /// 当前页面是否接收到了Post请求
        /// </summary>
        public static bool IsPost
        {
            get { return HttpContext.Current.Request.HttpMethod.Equals("POST"); }
        }

        /// <summary>
        /// 当前页面是否接收到了Get请求
        /// </summary>
        public static bool IsGet
        {
            get { return HttpContext.Current.Request.HttpMethod.Equals("GET"); }
        }

        /// <summary>
        /// 客户端请求当前页面的URL地址
        /// </summary>
        public static string CurURL
        {
            get
            {
                if (ConvertHelper.IsNull(HttpContext.Current.Request.Url))
                    return string.Empty;
                return HttpContext.Current.Request.Url.ToString();
            }
        }

        /// <summary>
        /// 客户端请求当前页面的原始URL地址，不带http://的
        /// </summary>
        public static string CurRawURL
        {
            get
            {
                if (ConvertHelper.IsNull(HttpContext.Current.Request.RawUrl))
                    return string.Empty;
                return HttpContext.Current.Request.RawUrl;
            }
        }

        /// <summary>
        /// 上次请求的URL地址，该URL地址链接到当前URL
        /// </summary>
        public static string ReferrerURL
        {
            get
            {
                if (ConvertHelper.IsNull(HttpContext.Current.Request.UrlReferrer))
                    return string.Empty;
                return HttpContext.Current.Request.UrlReferrer.ToString();
            }
        }

        /// <summary>
        /// 当前请求URL的主机名
        /// </summary>
        public static string CurHostName
        {
            get
            {
                if (ConvertHelper.IsNull(HttpContext.Current.Request.Url))
                    return string.Empty;
                return HttpContext.Current.Request.Url.Host;
            }
        }

        /// <summary>
        /// 当前请求URL的主机名和端口信息
        /// </summary>
        public static string CurFullHostName
        {
            get
            {
                if (!HttpContext.Current.Request.Url.IsDefaultPort)
                {
                    return string.Format("{0}:{1}", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port.ToString());
                }
                return CurHostName;
            }
        }

        /// <summary>
        /// 当前页面文件的完整物理路径
        /// </summary>
        public static string CurPhysicalPath
        {
            get
            {
                return HttpContext.Current.Request.PhysicalPath;
            }
        }

        /// <summary>
        /// 当前页面文件名
        /// </summary>
        public static string CurPageName
        {
            get
            {
                string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
                return urlArr[urlArr.Length - 1];
            }
        }

        /// <summary>
        /// 当前应用程序的完整物理路径
        /// </summary>
        public static string CurPhysicalApplicationPath
        {
            get
            {
                return HttpContext.Current.Request.PhysicalApplicationPath;
            }
        }

        /// <summary>
        /// 远程客户端的IP地址
        /// </summary>
        public static string ClientIP
        {
            get
            {
                string IP;
                try
                {
                    HttpRequest request = HttpContext.Current.Request;
                    if (request.ServerVariables["HTTP_VIA"] != null)
                    {
                        IP = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
                    }
                    else
                    {
                        IP = request.UserHostAddress;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                return IP;
                //return HttpContext.Current.Request.UserHostAddress;
            }
        }

        /// <summary>
        /// 获取客户端浏览器的版本语言"zh-cn"
        /// </summary>
        public static string ClientBSLanguage
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables.Get("HTTP_ACCEPT_LANGUAGE");
            }
        }

        /// <summary>
        /// 远程客户端的系统信息
        /// </summary>
        public static Hashtable ClientSysInfo
        {
            get
            {
                Hashtable ht = new Hashtable();
                ht.Add("IP", ClientIP);

                HttpBrowserCapabilities bc = new HttpBrowserCapabilities();
                bc = System.Web.HttpContext.Current.Request.Browser;
                ht.Add("OS", bc.Platform);  //操作系统
                ht.Add("BS", bc.Type);  //浏览器
                ht.Add("LANGUAGE", ClientBSLanguage);//获取客户端浏览器的版本语言"zh-cn"
                return ht;
            }
        }

        /// <summary>
        /// 远程客户端的DNS名称
        /// </summary>
        public static string ClientDNSName
        {
            get
            {
                return HttpContext.Current.Request.UserHostName;
            }
        }

        /// <summary>
        /// 获取当前请求Form对象
        /// </summary>
        public static NameValueCollection Form
        {
            get
            {
                return HttpContext.Current.Request.Form;
            }
        }

        /// <summary>
        /// 获取当前请求QueryString对象
        /// </summary>
        public static NameValueCollection QueryString
        {
            get
            {
                return HttpContext.Current.Request.QueryString;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取web服务器指定名称的变量值
        /// </summary>
        public static string GetServerVariable(string name)
        {
            if (HttpContext.Current.Request.ServerVariables[name] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.ServerVariables[name].ToString();
        }

        /// <summary>
        /// 获取指定名称的http头变量信息
        /// </summary>
        public static string GetHeadValue(string name)
        {
            if (HttpContext.Current.Request.Headers[name] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.Headers[name].ToString();
        }

        /// <summary>
        /// 获取string类型的url参数值
        /// </summary>
        public static string GetQueryString(string name)
        {
            return ConvertHelper.ToString(HttpContext.Current.Request.QueryString[name]);
        }

        /// <summary>
        /// 获取int类型的url参数值
        /// </summary>
        public static int GetQueryInt(string name, int defaultValue)
        {
            return ConvertHelper.ToInt(HttpContext.Current.Request.QueryString[name], defaultValue);
        }

        /// <summary>
        /// 获取int类型的url参数值
        /// </summary>
        public static Int64 GetQueryInt64(string name, Int64 defaultValue)
        {
            return ConvertHelper.ToInt64(HttpContext.Current.Request.QueryString[name], defaultValue);
        }

        /// <summary>
        /// 获取float类型的url参数值
        /// </summary>
        public static float GetQueryFloat(string name, float defaultValue)
        {
            return ConvertHelper.ToFloat(HttpContext.Current.Request.QueryString[name], defaultValue);
        }

        /// <summary>
        /// 获取decimal类型的url参数值
        /// </summary>
        public static decimal GetQueryDecimal(string name, decimal defaultValue)
        {
            return ConvertHelper.ToDecimal(HttpContext.Current.Request.QueryString[name], defaultValue);
        }

        /// <summary>
        /// 获取double类型的url参数值
        /// </summary>
        public static double GetQueryDouble(string name, double defaultValue)
        {
            return ConvertHelper.ToDouble(HttpContext.Current.Request.QueryString[name], defaultValue);
        }

        /// <summary>
        /// 获取DateTime类型的url参数值
        /// </summary>
        public static DateTime GetQueryDateTime(string name)
        {
            return ConvertHelper.ToDateTime(HttpContext.Current.Request.QueryString[name]);
        }

        /// <summary>
        /// 获取string类型的Form参数值
        /// </summary>
        public static string GetFormString(string name)
        {
            return ConvertHelper.ToString(HttpContext.Current.Request.Form[name]);
        }

        /// <summary>
        /// 获取int类型的Form参数值
        /// </summary>
        public static int GetFormInt(string name, int defaultValue)
        {
            return ConvertHelper.ToInt(HttpContext.Current.Request.Form[name], defaultValue);
        }

        /// <summary>
        /// 获取Int64类型的Form参数值
        /// </summary>
        public static Int64 GetFormInt64(string name, Int64 defaultValue)
        {
            return ConvertHelper.ToInt64(HttpContext.Current.Request.Form[name], defaultValue);
        }

        /// <summary>
        /// 获取float类型的Form参数值
        /// </summary>
        public static float GetFormFloat(string name, float defaultValue)
        {
            return ConvertHelper.ToFloat(HttpContext.Current.Request.Form[name], defaultValue);
        }

        /// <summary>
        /// 获取double类型的Form参数值
        /// </summary>
        public static double GetFormDouble(string name, double defaultValue)
        {
            return ConvertHelper.ToDouble(HttpContext.Current.Request.Form[name], defaultValue);
        }

        /// <summary>
        /// 获取decimal类型的Form参数值
        /// </summary>
        public static decimal GetFormDecimal(string name, decimal defaultValue)
        {
            return ConvertHelper.ToDecimal(HttpContext.Current.Request.Form[name], defaultValue);
        }

        /// <summary>
        /// 获取DateTime类型的Form参数值
        /// </summary>
        public static DateTime GetFormDateTime(string name)
        {
            return ConvertHelper.ToDateTime(HttpContext.Current.Request.Form[name]);
        }

        #endregion

        #region
        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            if ("".Equals(GetQueryString(strName)))
            {
                return GetFormString(strName);//2013.10.25截取空格
            }
            else
            {
                return GetQueryString(strName);
            }
        }


        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static int GetInt(string strName, int defValue)
        {
            if (GetQueryInt(strName, defValue) == defValue)
            {
                return GetFormInt(strName, defValue);
            }
            else
            {
                return GetQueryInt(strName, defValue);
            }
        }

        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static Int64 GetInt64(string strName, Int64 defValue)
        {
            if (GetQueryInt64(strName, defValue) == defValue)
            {
                return GetFormInt64(strName, defValue);
            }
            else
            {
                return GetQueryInt64(strName, defValue);
            }
        }

        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static int GetInt(string strName)
        {
            return GetInt(strName, 0);
        }


        public static Int64 GetInt64(string strName)
        {
            return GetInt64(strName, 0);
        }

        /// <summary>
        /// 获得指定Url或表单参数的Decimal类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static Decimal GetDecimal(string strName, Decimal defValue)
        {
            if (GetQueryDecimal(strName, defValue) == defValue)
            {
                return GetFormDecimal(strName, defValue);
            }
            else
            {
                return GetQueryDecimal(strName, defValue);
            }
        }

        /// <summary>
        /// 获得指定Url或表单参数的Decimal类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName"></param>
        /// <returns>如果没有返回默认值0</returns>
        public static Decimal GetDecimal(string strName)
        {
            return GetDecimal(strName, 0);
        }

        /// <summary>
        /// 获得指定Url或表单参数的Double类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static Double GetDouble(string strName, Double defValue)
        {
            if (GetQueryDouble(strName, defValue) == defValue)
            {
                return GetFormDouble(strName, defValue);
            }
            else
            {
                return GetQueryDouble(strName, defValue);
            }
        }

        /// <summary>
        /// 获取日期
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static DateTime? GetDateTime(string strName)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request[strName]))
            {
                return null;
            }
            else
            {
                var time = ConvertHelper.ToDateTime(HttpContext.Current.Request[strName]);
                if (time == DateTime.MinValue) return null;
                else
                {
                    return time;
                }

            }
        }
        #endregion
    }
}
