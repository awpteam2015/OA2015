using System;
using System.Collections.Generic;
using System.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;

namespace Project.Service.PermissionManager.Validate
{
    public class UserInfoValidate
    {
        #region
        private static readonly UserInfoValidate Instance = new UserInfoValidate();
        private readonly UserInfoRepository _userInfoRepository;
        private readonly UserDepartmentRepository _userDepartmentRepository;
        private readonly UserRoleRepository _userRoleRepository;

        private UserInfoValidate()
        {
            _userInfoRepository = new UserInfoRepository();
            _userDepartmentRepository = new UserDepartmentRepository();
            _userRoleRepository = new UserRoleRepository();
        }

        public static UserInfoValidate GetInstance()
        {
            return Instance;
        }

        #endregion

        /// <summary>
        /// 是否存在相同的会员卡号
        /// </summary>
        /// <param name="newUserCode"></param>
        /// <param name="pkId"></param>
        /// <returns></returns>
        public Tuple<bool, string> IsHasSameUserCode(string newUserCode, int pkId = 0)
        {
            var list = UserInfoService.GetInstance().GetList(new UserInfoEntity() { UserCode = newUserCode });
            if (pkId > 0 && list.Any(p => p.PkId != pkId))
            {
                return new Tuple<bool, string>(false, "存在重复的会员卡号！");
            }

            if (pkId <= 0 && list.Any())
            {
                return new Tuple<bool, string>(false, "存在重复的会员卡号！");
            }

            return new Tuple<bool, string>(true, "存在重复的会员卡号！");
        }


    }
}
