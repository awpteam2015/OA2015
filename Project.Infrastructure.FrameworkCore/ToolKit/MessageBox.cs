/*-----------------------------------------
 *开 发 者:  awp
 *创建日期:   2009-2-20
 *功能描述:  提示信息
 *-----------------------------------------*/

using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace  Project.Infrastructure.FrameworkCore.ToolKit
{
    /// <summary>
    /// 显示消息提示对话框。
    /// </summary>
    public sealed class MessageBox
    {

        private MessageBox()
        {
        }

        /// <summary>
        /// 当前页实例
        /// </summary>
        static Page CurrentPage
        {
            get
            {
                return (Page)HttpContext.Current.Handler;
            }
        }

        #region 公共方法
        /// <summary>
        ///  显示消息提示对话框,如果页面上不具有runat="server"的header那么将采用Response.Write的方式输出脚本
        /// </summary>
        /// <param name="msg">要显示的消息</param>
        public static void Alert(string msg)
        {
            msg = ReplaceStrToScript(RemoveRNString(msg));
            RegisterScript("alert('" + msg + "');");
        }

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="msg">提示信息</param>
        public static void AlertConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            Control.Attributes.Add("onclick", "return confirm('" + ReplaceStrToScript(RemoveRNString(msg)) + "');");
        }
        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void AlertAndRedirect(string msg, string url)
        {
            msg = ReplaceStrToScript(RemoveRNString(msg));
            url = ReplaceStrToScript(RemoveRNString(url));
            string script = string.Format("alert('{0}');", msg);
            script += string.Format("window.location.href='{0}';", url);
            string html = "<html><head><title>提示信息!</title>" + getScript(script) + "</head><body>" + msg + "，请点击<a href=\"" + url + "\">继续</a><body></html>";
            HttpContext.Current.Response.Write(html.Trim());
            HttpContext.Current.Response.End();
            //RegisterScript(script);
        }
        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void AlertAndBack(string msg)
        {
            msg = ReplaceStrToScript(RemoveRNString(msg));
            string script = string.Format("alert('{0}');history.go(-1)", msg);
            RegisterScript(script);
        }

        /// <summary>
        /// 向客户端显示脚本信息,不需要在写&lt;script language=\"javascript\"&gt;
        /// </summary>
        /// <param name="script">脚本的主体部分</param>
        public static void RegisterScript(string script)
        {
            script = getScript(script);
            if (CurrentPage.Header != null)
            {
                LiteralControl hc = CurrentPage.Header.FindControl("scriptRegister") as LiteralControl;
                if (hc == null)
                {
                    hc = new LiteralControl();
                    CurrentPage.Header.Controls.Add(hc);
                }
                hc.Text = script;

            }
            else
            {
                HttpContext.Current.Response.Write(script);
            }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 获得脚本申明
        /// </summary>
        private static string getScript(string script)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language=\"javascript\">");
            Builder.Append(script);
            Builder.Append("</script>");
            return Builder.ToString();
        }


        /// <summary>
        /// 删除字符串的回车/换行
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string RemoveRNString(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            return str.Replace("\r", "").Replace("\n", "");
        }
        /// <summary>
        /// 删除所有空白字符比如换行符 空格符等
        /// </summary>
        /// <param name="sourcestr"></param>
        /// <returns></returns>
        private static string RemoveSpaceString(string sourcestr)
        {
            return Regex.Replace(sourcestr, "\\s", "");
        }


        /// <summary>
        /// 为脚本替换特殊字符串将字符串中的引号进行转义
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string ReplaceStrToScript(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("'", "\\'");
            str = str.Replace("\"", "\\\"");
            return str;
        }
        #endregion
    }
}
