using System.Collections.Generic;
using Project.Model.PermissionManager;

namespace Project.Service.PermissionManager.DTO
{
    /// <summary>
    /// 登录用户信息及权限信息
    /// </summary>
    public class LoginUserInfoDTO
    {
        public LoginUserInfoDTO()
        {
            PermissionCodeList = new List<int>();
        }

        public string UserCode { get; set; }
        public string UserName { get; set; }

        public string ClientIp { get; set; }

        /// <summary>
        /// 授予的权限代号
        /// </summary>
        public IList<int>  PermissionCodeList { get; set; }


        public ISet<UserDepartmentEntity> UserDepartmentList { get; set; }
    }
}
