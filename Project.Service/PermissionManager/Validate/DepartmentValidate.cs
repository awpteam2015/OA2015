using System;
using System.Collections.Generic;
using System.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;

namespace Project.Service.PermissionManager.Validate
{
    public class DepartmentValidate
    {
        #region
        private static readonly DepartmentValidate Instance = new DepartmentValidate();
        private readonly DepartmentRepository _DepartmentRepository;


        private DepartmentValidate()
        {
            _DepartmentRepository = new DepartmentRepository();
        }

        public static DepartmentValidate GetInstance()
        {
            return Instance;
        }

        #endregion

        /// <summary>
        /// 是否存在相同的部门
        /// </summary>
        /// <param name="newDepartmentCode"></param>
        /// <param name="pkId"></param>
        /// <returns></returns>
        public Tuple<bool, string> IsHasSameDepartmentCode(string newDepartmentCode, int pkId = 0)
        {
            var list = DepartmentService.GetInstance().GetList(new DepartmentEntity() { DepartmentCode = newDepartmentCode });
            if (pkId > 0 && list.Any(p => p.PkId != pkId))
            {
                return new Tuple<bool, string>(false, "存在重复的部门编码！");
            }

            if (pkId <= 0 && list.Any())
            {
                return new Tuple<bool, string>(false, "存在重复的部门编码！");
            }

            return new Tuple<bool, string>(true, "存在重复的部门编码！");
        }


    }
}
