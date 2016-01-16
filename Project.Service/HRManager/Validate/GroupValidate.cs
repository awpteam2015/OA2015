using System;
using System.Collections.Generic;
using System.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;


namespace Project.Service.HRManager.Validate
{
    public class GroupValidate
    {
        #region
        private static readonly GroupValidate Instance = new GroupValidate();
        private readonly GroupRepository _groupRepository;

        private GroupValidate()
        {
            _groupRepository = new GroupRepository();
        }

        public static GroupValidate GetInstance()
        {
            return Instance;
        }

        #endregion

        /// <summary>
        /// 是否存在相同的组编号
        /// </summary>
        /// <param name="newUserCode"></param>
        /// <param name="pkId"></param>
        /// <returns></returns>
        public Tuple<bool, string> IsHasSameGroupCode(string newGroupCode, int pkId = 0)
        {
            var list = GroupService.GetInstance().GetList(new GroupEntity() { GroupCode = newGroupCode });
            if (pkId > 0 && list.Any(p => p.PkId != pkId))
            {
                return new Tuple<bool, string>(false, "存在重复的组编号！");
            }

            if (pkId <= 0 && list.Any())
            {
                return new Tuple<bool, string>(false, "存在重复的组编号！");
            }

            return new Tuple<bool, string>(true, "存在重复的员工编号！");
        }
    }
}
