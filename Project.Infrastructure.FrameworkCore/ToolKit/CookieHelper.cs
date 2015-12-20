/*******************************************************
版权所有：
用    途：底层结构类库-Cookie处理类。

结构组成：

说    明：Set 与　GetValue 它们不能在同一个方法中使用，必须在Set 返回给客户端之后GetValue 才能取到最新的值，否则取到的是旧的历史值

作    者：李伟伟

创建日期：2009-02-04
历史记录：

*****************************************************
修改人员：               修改日期： 
修改说明：   
*******************************************************/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace Project.Infrastructure.FrameworkCore.ToolKit
{
    /// <summary>
    /// cookie帮助类
    /// </summary>
    public class CookieHelper
    {
        private static string strDomain = ConfigHelper.GetString("Domain");

        /// <summary>
        /// 添加购物车到Cookie
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="iExpires"></param>
        public static void SaveCookieCart(string strValue, int iExpires)
        {
            var cookie = HttpContext.Current.Request.Cookies["harborhousecart"];
            if (cookie != null)
            {
                cookie.Value = HttpUtility.UrlEncode(strValue.Trim());
                cookie.Expires = DateTime.Now.AddSeconds(iExpires);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                var objCookie = new HttpCookie("harborhousecart");
                objCookie.Value = HttpUtility.UrlEncode(strValue.Trim());
                objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                HttpContext.Current.Response.Cookies.Add(objCookie);
            }
        }

        /// <summary>
        /// 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        /// </summary>
        /// <param name="strCookieName">COOKIE对象名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        /// <param name="strValue">COOKIE对象Value值</param>
        public static void Set(string strCookieName, int iExpires, string strValue)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            objCookie.Value = HttpUtility.UrlEncode(strValue.Trim());
            if (!string.IsNullOrEmpty(strDomain))
            {
                objCookie.Domain = strDomain;
            }
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// 更新Cookie的值
        /// </summary>
        /// <param name="strCookieName"></param>
        /// <param name="iExpires"></param>
        /// <param name="strValue"></param>
        public static void Update(string strCookieName, int iExpires, string strValue)
        {
            var cookie = HttpContext.Current.Response.Cookies[strCookieName];
            if (cookie != null)
            {
                cookie.Value = strValue;
                cookie.Expires = DateTime.Now.AddSeconds(iExpires);
            }
        }

        /// <summary>
        /// 创建COOKIE对象并赋多个KEY键值
        /// 设键/值如下：
        /// NameValueCollection myCol = new NameValueCollection();
        /// myCol.Add("red", "rojo");
        /// myCol.Add("green", "verde");
        /// myCol.Add("blue", "azul");
        /// myCol.Add("red", "rouge");   结果“red:rojo,rouge；green:verde；blue:azul”
        /// </summary>
        /// <param name="strCookieName">COOKIE对象名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        /// <param name="KeyValue">键/值对集合</param>
        public static void Set(string strCookieName, int iExpires, NameValueCollection KeyValue)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            foreach (String key in KeyValue.AllKeys)
            {
                objCookie[key] = HttpUtility.UrlEncode(KeyValue[key].Trim());
            }
            if (!string.IsNullOrEmpty(strDomain))
            {
                objCookie.Domain = strDomain;
            }
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        /// </summary>
        /// <param name="strCookieName">COOKIE对象名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        /// <param name="strDomain">作用域</param>
        /// <param name="strValue">COOKIE对象Value值</param>
        public static void Set(string strCookieName, int iExpires, string strValue, string strDomain)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            objCookie.Value = HttpUtility.UrlEncode(strValue.Trim());
            objCookie.Domain = strDomain.Trim();
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// 创建COOKIE对象并赋多个KEY键值
        /// 设键/值如下：
        /// NameValueCollection myCol = new NameValueCollection();
        /// myCol.Add("red", "rojo");
        /// myCol.Add("green", "verde");
        /// myCol.Add("blue", "azul");
        /// myCol.Add("red", "rouge");   结果“red:rojo,rouge；green:verde；blue:azul”
        /// </summary>
        /// <param name="strCookieName">COOKIE对象名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        /// <param name="strDomain">作用域</param>
        /// <param name="KeyValue">键/值对集合</param>
        public static void Set(string strCookieName, int iExpires, NameValueCollection KeyValue, string strDomain)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            foreach (String key in KeyValue.AllKeys)
            {
                objCookie[key] = HttpUtility.UrlEncode(KeyValue[key].Trim());
            }
            objCookie.Domain = strDomain.Trim();
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// 判断Cookie某个对象和Value值，是否存在
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <returns>KeyNameList值，通过NameValueCollection设置Cookie的KeyName</returns>
        public static bool IsCookies(string strCookieName, List<string> KeyNameList)
        {
            bool result = false;
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return result;
            }
            else
            {
                foreach (string strKeyName in KeyNameList)
                {
                    string strObjValue = HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName].Value);
                    string strKeyName2 = strKeyName + "=";
                    if (strObjValue.IndexOf(strKeyName2) == -1)
                    {
                        return result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 读取Cookie某个对象的Value值，返回Value值，如果对象本就不存在，则返回string.Empty
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <returns>Value值，如果对象本就不存在，则返回string.Empty</returns>
        public static string GetValue(string strCookieName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return string.Empty;
            }
            else
            {
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName].Value);
            }
        }

        /// <summary>
        /// 读取Cookie某个对象的某个Key键的键值，返回Key键值，如果对象本就不存在，则返回字符串"CookieNonexistence"，如果Key键不存在，则返回字符串"KeyNonexistence"
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <param name="strKeyName">Key键名</param>
        /// <returns>Key键值，如果对象本就不存在，则返回字符串"CookieNonexistence"，如果Key键不存在，则返回字符串"KeyNonexistence"</returns>
        public static string GetValue(string strCookieName, string strKeyName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return "CookieNonexistence";
            }
            else
            {
                string strObjValue = HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName].Value);
                string strKeyName2 = strKeyName + "=";
                if (strObjValue.IndexOf(strKeyName2) == -1)
                {
                    return "KeyNonexistence";
                }
                else
                {
                    return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName][strKeyName]);
                }
            }
        }


        /// <summary>
        /// 修改某个COOKIE对象某个Key键的键值 或 给某个COOKIE对象添加Key键 都调用本方法，操作成功返回字符串"success"，如果对象本就不存在，则返回字符串"CookieNonexistence"。
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <param name="strKeyName">Key键名</param>
        /// <param name="KeyValue">Key键值</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)。注意：虽是修改功能，实则重建覆盖，所以时间也要重设，因为没办法获得旧的有效期</param>
        /// <returns>如果对象本就不存在，则返回字符串"CookieNonexistence"，如果操作成功返回字符串"success"。</returns>
        public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return "CookieNonexistence";
            }
            else
            {
                HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
                objCookie[strKeyName] = HttpUtility.UrlEncode(KeyValue.Trim());
                if (!string.IsNullOrEmpty(strDomain))
                {
                    objCookie.Domain = strDomain;
                }
                if (iExpires > 0)
                {
                    if (iExpires == 1)
                    {
                        objCookie.Expires = DateTime.MaxValue;
                    }
                    else
                    {
                        objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                    }
                }
                HttpContext.Current.Response.Cookies.Add(objCookie);
                return "success";
            }
        }


        /// <summary>
        /// 删除COOKIE对象
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        public static void Del(string strCookieName)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            if (!string.IsNullOrEmpty(strDomain))
            {
                objCookie.Domain = strDomain;
            }
            objCookie.Expires = DateTime.Now.AddYears(-5);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }


        /// <summary>
        /// 删除某个COOKIE对象某个Key子键，操作成功返回字符串"success"，如果对象本就不存在，则返回字符串"CookieNonexistence"
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <param name="strKeyName">Key键名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)。注意：虽是修改功能，实则重建覆盖，所以时间也要重设，因为没办法获得旧的有效期</param>
        /// <returns>如果对象本就不存在，则返回字符串"CookieNonexistence"，如果操作成功返回字符串"success"。</returns>
        public static string Del(string strCookieName, string strKeyName, int iExpires)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return "CookieNonexistence";
            }
            else
            {
                HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
                objCookie.Values.Remove(strKeyName);
                if (!string.IsNullOrEmpty(strDomain))
                {
                    objCookie.Domain = strDomain;
                }
                if (iExpires > 0)
                {
                    if (iExpires == 1)
                    {
                        objCookie.Expires = DateTime.MaxValue;
                    }
                    else
                    {
                        objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                    }
                }
                HttpContext.Current.Response.Cookies.Add(objCookie);
                return "success";
            }
        }


        #region ClearCookie ：删除应用程序中所有可用 Cookie
        //Request.Cookies.Clear()这个方法并不是删除Cookie,删除 Cookie（即从用户的硬盘中物理移除 Cookie）是修改 Cookie 的一种形式。 
        //由于 Cookie 在用户的计算机中，因此无法将其直接移除。但是，可以让浏览器来为您删除 Cookie。该技术是创建一个与要删除的 Cookie 
        //同名的新 Cookie， 并将该 Cookie 的到期日期设置为早于当前日期的某个日期。 当浏览器检查 Cookie 的到期日期时，浏览器便会丢弃这个现已过期的 Cookie
        public static void ClearAll()
        {
            HttpCookie cookie;
            string cookieName;
            int limit = HttpContext.Current.Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = HttpContext.Current.Request.Cookies[i].Name;
                cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        #endregion
    }
}
