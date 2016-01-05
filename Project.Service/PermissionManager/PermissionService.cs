using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NHibernate.Util;
using Project.Model.PermissionManager;
using Project.Service.PermissionManager.DTO;

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
        private static List<PermissionFunctionDetailDTO> _allPermissionFunctionList = new List<PermissionFunctionDetailDTO>();

        public static PermissionService GetInstance()
        {
            return Instance;
        }

        public bool IsAdmin(string userCode)
        {
            return userCode.ToLower() == "admin";
        }

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public IList<PermissionFunctionDetailDTO> GetAllPermissionFunction()
        {
            //if (_allPermissionFunctionList == null || !_allPermissionFunctionList.Any())
            //{
                var list = FunctionService.GetInstance().GetFunctionDetailList(new FunctionDetailEntity());
                var tempList = new List<PermissionFunctionDetailDTO>();
                list.ForEach(p =>
                {
                    tempList.Add(Mapper.Map<FunctionDetailEntity, PermissionFunctionDetailDTO>(p));
                });
                _allPermissionFunctionList = tempList;
          //  }
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
