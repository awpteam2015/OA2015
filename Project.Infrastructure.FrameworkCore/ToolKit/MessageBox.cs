/*-----------------------------------------
 *�� �� ��:  awp
 *��������:   2009-2-20
 *��������:  ��ʾ��Ϣ
 *-----------------------------------------*/

using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace  Project.Infrastructure.FrameworkCore.ToolKit
{
    /// <summary>
    /// ��ʾ��Ϣ��ʾ�Ի���
    /// </summary>
    public sealed class MessageBox
    {

        private MessageBox()
        {
        }

        /// <summary>
        /// ��ǰҳʵ��
        /// </summary>
        static Page CurrentPage
        {
            get
            {
                return (Page)HttpContext.Current.Handler;
            }
        }

        #region ��������
        /// <summary>
        ///  ��ʾ��Ϣ��ʾ�Ի���,���ҳ���ϲ�����runat="server"��header��ô������Response.Write�ķ�ʽ����ű�
        /// </summary>
        /// <param name="msg">Ҫ��ʾ����Ϣ</param>
        public static void Alert(string msg)
        {
            msg = ReplaceStrToScript(RemoveRNString(msg));
            RegisterScript("alert('" + msg + "');");
        }

        /// <summary>
        /// �ؼ���� ��Ϣȷ����ʾ��
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void AlertConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            Control.Attributes.Add("onclick", "return confirm('" + ReplaceStrToScript(RemoveRNString(msg)) + "');");
        }
        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲�����ҳ����ת
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="url">��ת��Ŀ��URL</param>
        public static void AlertAndRedirect(string msg, string url)
        {
            msg = ReplaceStrToScript(RemoveRNString(msg));
            url = ReplaceStrToScript(RemoveRNString(url));
            string script = string.Format("alert('{0}');", msg);
            script += string.Format("window.location.href='{0}';", url);
            string html = "<html><head><title>��ʾ��Ϣ!</title>" + getScript(script) + "</head><body>" + msg + "������<a href=\"" + url + "\">����</a><body></html>";
            HttpContext.Current.Response.Write(html.Trim());
            HttpContext.Current.Response.End();
            //RegisterScript(script);
        }
        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲�����ҳ����ת
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="url">��ת��Ŀ��URL</param>
        public static void AlertAndBack(string msg)
        {
            msg = ReplaceStrToScript(RemoveRNString(msg));
            string script = string.Format("alert('{0}');history.go(-1)", msg);
            RegisterScript(script);
        }

        /// <summary>
        /// ��ͻ�����ʾ�ű���Ϣ,����Ҫ��д&lt;script language=\"javascript\"&gt;
        /// </summary>
        /// <param name="script">�ű������岿��</param>
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

        #region ˽�з���
        /// <summary>
        /// ��ýű�����
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
        /// ɾ���ַ����Ļس�/����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string RemoveRNString(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            return str.Replace("\r", "").Replace("\n", "");
        }
        /// <summary>
        /// ɾ�����пհ��ַ����绻�з� �ո����
        /// </summary>
        /// <param name="sourcestr"></param>
        /// <returns></returns>
        private static string RemoveSpaceString(string sourcestr)
        {
            return Regex.Replace(sourcestr, "\\s", "");
        }


        /// <summary>
        /// Ϊ�ű��滻�����ַ������ַ����е����Ž���ת��
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
