using System;
using System.Globalization;
using System.IO;
using System.Web;
using log4net;
using log4net.Config;
using log4net.Util;

namespace Project.Infrastructure.FrameworkCore.Logging
{
    public enum LogType
    {
        InfoLogger = 0,
        ErrorLogger = 1,
        ExceptionLogger = 2,
        SuccessLogger = 3,
    }

    public enum LogLevel
    {
        Error = 0,
        Success = 1,
    }



    /// <summary>
    /// 日志记录帮助类
    /// </summary>
    public static class LoggerHelper
    {
        private static ILog _log;
        static LoggerHelper()
        {
            if (HttpContext.Current != null)
            {
                var path = HttpContext.Current.Server.MapPath("~/bin/Config/log4net.config");
                var s = XmlConfigurator.Configure(new FileInfo(path));
            }
            else
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + @"bin\Config\log4net.config";
                XmlConfigurator.Configure(new FileInfo(path));
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            if (HttpContext.Current != null)
            {
                var path = HttpContext.Current.Server.MapPath("bin/Config/log4net.config");
                XmlConfigurator.Configure(new FileInfo(path));
            }
            else
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + @"bin\Config\log4net.config";
                XmlConfigurator.Configure(new FileInfo(path));
            }
        }
        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="message">信息</param>
        public static void Info(object message)
        {
            _log = LogManager.GetLogger(LogType.InfoLogger.ToString());
            _log.Info(message);
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="name">日志记录名称</param>
        /// <param name="message">信息</param>
        public static void Info(LogType name, object message)
        {
            _log = LogManager.GetLogger(name.ToString());
            _log.Info(message);
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="name">日志记录名称</param>
        /// <param name="format">错误信息</param>
        /// <param name="args">异常信息</param>
        public static void InfoFormat(LogType name, string format, params object[] args)
        {
            _log = LogManager.GetLogger(name.ToString());
            _log.Info(new SystemStringFormat(CultureInfo.InvariantCulture, format, args));
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void InfoFormat(string name, string format, params object[] args)
        {
            _log = LogManager.GetLogger(name);
            _log.Info(new SystemStringFormat(CultureInfo.InvariantCulture, format, args));
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        public static void Error(object message)
        {
            _log = LogManager.GetLogger(LogType.ErrorLogger.ToString());
            _log.Error(message);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="name">日志记录名称</param>
        /// <param name="message">错误信息</param>
        public static void Error(LogType name, object message)
        {
            _log = LogManager.GetLogger(name.ToString());
            _log.Error(message);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="name">日志记录名称</param>
        /// <param name="format">错误信息</param>
        /// <param name="args">错误信息参数</param>
        public static void ErrorFormat(LogType name, string format, params object[] args)
        {
            _log = LogManager.GetLogger(name.ToString());
            _log.Error(new SystemStringFormat(CultureInfo.InvariantCulture, format, args));
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="name">日志记录名称</param>
        /// <param name="format">错误信息</param>
        /// <param name="args">错误信息参数</param>
        public static void ErrorFormat(string name, string format, params object[] args)
        {
            _log = LogManager.GetLogger(name);
            _log.Error(new SystemStringFormat(CultureInfo.InvariantCulture, format, args));
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message"></param>
        public static void SendEmail(object message)
        {
            _log = LogManager.GetLogger("SmtpLogger");
            _log.Error(message);
        }
    }
}