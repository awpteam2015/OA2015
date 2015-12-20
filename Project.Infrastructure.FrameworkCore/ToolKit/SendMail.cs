using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using Project.Infrastructure.FrameworkCore.ToolKit.StringHandler;

namespace Project.Infrastructure.FrameworkCore.ToolKit
{
    public class SendMail
    {
        /// <summary>
        /// 邮件服务器
        /// </summary>
        public string MailHost { get; private set; }
        /// <summary>
        /// 发出的邮箱
        /// </summary>
        public string MailForm { get; private set; }
        /// <summary>
        /// 发件箱密码
        /// </summary>
        public string MailFormPwd { get; private set; }

        public SendMail()
        {
            MailHost = ConfigurationManager.AppSettings["MailHost"];
            MailForm = ConfigurationManager.AppSettings["MailForm"];
            MailFormPwd = ConfigurationManager.AppSettings["MailFormPwd"];
        }
        //----------------------发送一封邮件到用户的邮箱------------------------------

        /// <summary>
        /// 发送一封邮件到用户邮箱
        /// </summary>
        /// <param name="toUser">收件地址</param>
        /// <param name="fromUser">发件地址</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public bool Send(string toUser, string fromUser, string subject, string content)
        {
            var from = new MailAddress(MailForm, "ROM");
            var to = new MailAddress(toUser, toUser);
            var client = new SmtpClient
                             {
                                 Host = MailHost,
                                 UseDefaultCredentials = false,
                                 Credentials = new NetworkCredential(MailForm, MailFormPwd),
                                 DeliveryMethod = SmtpDeliveryMethod.Network
                             };
            var message = new MailMessage(from, to)
                              {
                                  Subject = subject,
                                  Body = content + "<br/>ROM地址:" + ConfigurationManager.AppSettings["ROMURL"],
                                  BodyEncoding = Encoding.UTF8,
                                  IsBodyHtml = true
                              };
            client.Send(message);
            return true;
        }
        /// <summary>
        /// 发送一封邮件到用户邮箱,邮件体是一个HTML页的内容
        /// </summary>
        /// <param name="toUser">收件地址</param>
        /// <param name="fromUser">发件地址</param>
        /// <param name="subject">主题</param>
        /// <param name="htmlfilePath">要发送的HTML文件</param><example>C:\\mail.html</example>
        /// <param name="dict">要替换的占位符</param>
        /// <returns></returns>
        public bool Send(string toUser, string fromUser, string subject, string htmlfilePath, Dictionary<string, string> dict)
        {
            var allCodeRequest = (HttpWebRequest)WebRequest.Create(htmlfilePath);
            WebResponse allCodeResponse = allCodeRequest.GetResponse();
            var theReader = new StreamReader(allCodeResponse.GetResponseStream());
            string allCode = theReader.ReadToEnd();
            theReader.Close();
            if (dict != null)
            {
                allCode = dict.Aggregate(allCode, (current, entry) => current.Replace(entry.Key, entry.Value));
            }
            var from = new MailAddress(MailForm, "ROM");
            var to = new MailAddress(toUser, toUser);
            var client = new SmtpClient
                             {
                                 Host = MailHost,
                                 UseDefaultCredentials = false,
                                 Credentials = new NetworkCredential(MailForm, MailFormPwd),
                                 DeliveryMethod = SmtpDeliveryMethod.Network
                             };
            var message = new MailMessage(from, to) { Subject = subject, Body = allCode, BodyEncoding = Encoding.UTF8, IsBodyHtml = true };
            client.Send(message);
            return true;
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="toUser">收件人地址</param>
        /// <param name="subject">邮件主题</param>
        /// <returns>发送是否成功</returns>
        public Tuple<bool, string> SendAuth(string toUser, string subject)
        {
            var authCode = StringHelper.GetRnd(6, true, false, false, false, "");
            var from = new MailAddress(MailForm, ConfigurationManager.AppSettings["StoreName"]);
            var to = new MailAddress(toUser, toUser);
            var client = new SmtpClient
            {
                Host = MailHost,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(MailForm, MailFormPwd),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = "验证码为" + authCode + "（客服人员绝不会索取此验证码，切勿告知他人），有问题请致电400-888-1916【Harbor House】",
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };

            try
            {
                client.Send(message);
                return new Tuple<bool, string>(true, authCode);
            }
            catch
            {
                return new Tuple<bool, string>(false, "发送邮件过程中发生了网络异常");
            }
        }

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="toUser">收件人地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容</param>
        /// <returns>发送是否成功</returns>
        public bool GoSend(string toUser, string subject, string content)
        {
            if (string.IsNullOrEmpty(toUser))
            {
                return false;
            }

            var from = new MailAddress(MailForm, "ROM");
            var to = new MailAddress(toUser, toUser);
            var client = new SmtpClient
                             {
                                 Host = MailHost,
                                 UseDefaultCredentials = false,
                                 Credentials = new NetworkCredential(MailForm, MailFormPwd),
                                 DeliveryMethod = SmtpDeliveryMethod.Network
                             };
            var message = new MailMessage(from, to)
                              {
                                  Subject = subject,
                                  Body = string.Format("{0}<br/>ROM地址:<a href='{1}'>{1}</a>", content, ConfigurationManager.AppSettings["ROMURL"]),
                                  BodyEncoding = Encoding.UTF8,
                                  IsBodyHtml = true
                              };

            try
            {
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 异步发送电子邮件
        /// </summary>
        /// <param name="toUser">收件人地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容</param>
        public void GoSendAsync(string toUser, string subject, string content)
        {
            ThreadPool.QueueUserWorkItem(x => GoSend(toUser, subject, content));
        }

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="toUser">收件人地址 多个时用“,”隔开</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容</param>
        /// <param name="cc">抄送人地址 多个时用“,”隔开</param>
        /// <returns></returns>
        public bool GoSend(string toUser, string subject, string content, string cc)
        {
            var from = new MailAddress(MailForm, "ROM");

            var client = new SmtpClient
                             {
                                 Host = MailHost,
                                 UseDefaultCredentials = false,
                                 Credentials = new NetworkCredential(MailForm, MailFormPwd),
                                 DeliveryMethod = SmtpDeliveryMethod.Network
                             };
            var message = new MailMessage { From = @from };
            message.To.Add(toUser);
            if (cc.Length != 0)
                message.CC.Add(cc);
            message.Subject = subject;
            message.Body = content + "<br/>ROM地址:" + ConfigurationManager.AppSettings["ROMURL"];
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            try
            {
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="toUser">收件人地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件模板内容</param>
        /// <param name="dict">键值</param>
        /// <returns>邮件发送是否成功</returns>
        public bool GoSend(string toUser, string subject, string content, Dictionary<string, string> dict)
        {
            string emailCode = content;
            if (dict != null)
            {
                emailCode = dict.Aggregate(emailCode, (current, entry) => current.Replace(entry.Key, entry.Value));
            }
            bool isWin = GoSend(toUser, subject, emailCode);
            return isWin;
        }
    }
}
