using System;

namespace  Project.Infrastructure.FrameworkCore.ToolKit.NetWorkHandler
{
    /// <summary>
    /// 域名操作
    /// </summary>
    public class DomainsUtil
    {
        /// <summary>
        /// 顶级域名信息
        /// </summary>
        private static string[] TopLevelDomains;
        static DomainsUtil()
        {
            TopLevelDomains = new string[] { ".com.cn", ".edu.cn", ".net.cn", ".org.cn", ".co.jp", 
                ".gov.cn", ".co.uk", "ac.cn", ".edu", ".tv", ".info", ".com", ".ac", ".ag", ".am", 
                ".at", ".be", ".biz", ".bz", ".cc", ".cn", ".com", ".de", ".es", ".eu", ".fm", ".gs", 
                ".hk", ".in", ".info", ".io", ".it", ".jp", ".la", ".md", ".ms", ".name", ".net", ".nl", ".nu", 
                ".org", ".pl", ".ru", ".sc", ".se", ".sg", ".sh", ".tc", ".tk", ".tv", ".tw", ".us", ".co", ".uk", 
                ".vc", ".vg", ".ws", ".il", ".li", ".nz" };
        }

        /// <summary>
        /// 获取域名主机信息：mail.163.com
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHostDomain(string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;

            string hostDomain = string.Empty;
            try
            {
                Uri uri = new Uri(url);
                hostDomain = uri.Host;
            }
            catch
            {
                return string.Empty;
            }
            return hostDomain;
        }

        /// <summary>
        /// 获取主域名信息：163.com
        /// </summary>
        /// <param name="fullDomain"></param>
        /// <returns></returns>
        public static string GetMainDomain(string hostDomain)
        {
            if (string.IsNullOrEmpty(hostDomain)) return string.Empty;

            string mainDomain = string.Empty;
            foreach (string domain in TopLevelDomains)
            {
                if (hostDomain.LastIndexOf(domain) != -1)
                {
                    mainDomain = hostDomain.Replace(domain, string.Empty);
                    break;
                }
            }

            int dotIndex = mainDomain.LastIndexOf(".");
            if (dotIndex != -1)
                mainDomain = hostDomain.Substring(dotIndex + 1);
            else
                mainDomain = hostDomain;
            return mainDomain;
        }
    }
}
