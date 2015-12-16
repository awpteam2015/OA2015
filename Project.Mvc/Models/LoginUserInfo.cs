using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Mvc.Models
{
    /// <summary>
    /// 登录用户信息及权限信息
    /// </summary>
    public class LoginUserInfo
    {
        public LoginUserInfo()
        {
            PermissionCodeList = new List<string>();
        }

        public string UserCode { get; set; }
        public string UserName { get; set; }

        public string ClientIp { get; set; }

        /// <summary>
        /// 授予的权限代号
        /// </summary>
        public List<string> PermissionCodeList { get; set; }
    }
}
