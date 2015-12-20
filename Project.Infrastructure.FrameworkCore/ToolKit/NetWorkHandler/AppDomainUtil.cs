using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace  Project.Infrastructure.FrameworkCore.ToolKit.NetWorkHandler
{
    /// <summary>
    /// 路径处理
    /// </summary>
    public class AppDomainUtil
    {
        /// <summary>
        /// 获取web站点下的目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted) //如果是web环境
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Server.MapPath(path);
                else
                    return HttpRuntime.AppDomainAppPath + path.Replace("~", string.Empty).Replace('/', '\\');
            }
            else
            {
                string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
                string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
                return assemblyDirPath + path.Replace("~", string.Empty).Replace('/', '\\');
            }
        }
    }
}
