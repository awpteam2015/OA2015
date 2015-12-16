using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model.PermissionManager;

namespace Project.Service.PermissionManager
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public class PermissionService
    {
        private static readonly PermissionService Instance = new PermissionService();

        /// <summary>
        /// 所有权限代号
        /// </summary>
        private static List<PermissionFunctionEntity> _allPermissionFunctionList = new List<PermissionFunctionEntity>();

        public static PermissionService GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public IList<PermissionFunctionEntity> GetAllPermissionFunction()
        {
            if (_allPermissionFunctionList == null || !_allPermissionFunctionList.Any())
            {
                //从数据库读取
                _allPermissionFunctionList = new List<PermissionFunctionEntity>();
            }
            return _allPermissionFunctionList;
        }

        /// <summary>
        /// 清空所有权限代号
        /// </summary>
        public void ClearAllPermissionFunction()
        {
            _allPermissionFunctionList = null;
        }

    }
}
