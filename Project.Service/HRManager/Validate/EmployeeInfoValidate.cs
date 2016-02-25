using System;
using System.Collections.Generic;
using System.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;


namespace Project.Service.HRManager.Validate
{
    public class EmployeeInfoValidate
    {
        #region
        private static readonly EmployeeInfoValidate Instance = new EmployeeInfoValidate();
        private readonly EmployeeInfoRepository _employeeInfoRepository;

        private EmployeeInfoValidate()
        {
            _employeeInfoRepository = new EmployeeInfoRepository();
        }

        public static EmployeeInfoValidate GetInstance()
        {
            return Instance;
        }

        #endregion

        /// <summary>
        /// 是否存在相同的员工编号
        /// </summary>
        /// <param name="newUserCode"></param>
        /// <param name="pkId"></param>
        /// <returns></returns>
        public Tuple<bool, string> IsHasSameEmployeeCode(string newEmployeeCode, int pkId = 0)
        {
            var list = EmployeeInfoService.GetInstance().GetList(new EmployeeInfoEntity() { EmployeeCode = newEmployeeCode });
            if (pkId > 0 && list.Any(p => p.PkId != pkId))
            {
                return new Tuple<bool, string>(false, "存在重复的员工编号！");
            }

            if (pkId <= 0 && list.Any())
            {
                return new Tuple<bool, string>(false, "存在重复的员工编号！");
            }

            return new Tuple<bool, string>(true, "");
        }

        /// <summary>
        /// 是否存在相同的员工编号
        /// </summary>
        /// <param name="newUserCode"></param>
        /// <param name="pkId"></param>
        /// <returns></returns>
        public Tuple<bool, string> IsHasSameCertNo(string newCertNo, int pkId = 0)
        {
            if (string.IsNullOrEmpty(newCertNo) || string.IsNullOrWhiteSpace(newCertNo))
                return new Tuple<bool, string>(true, "");
            var list = EmployeeInfoService.GetInstance().GetList(new EmployeeInfoEntity() { CertNo = newCertNo });
            if (pkId > 0 && list.Any(p => p.PkId != pkId))
            {
                return new Tuple<bool, string>(false, "存在重复的身份证号！");
            }

            if (pkId <= 0 && list.Any())
            {
                return new Tuple<bool, string>(false, "存在重复的身份证号！");
            }

            return new Tuple<bool, string>(true, "");
        }

        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public EmployeeInfoEntity GetModelByCertNo(System.String newCertNo)
        {
            if (string.IsNullOrEmpty(newCertNo) || string.IsNullOrWhiteSpace(newCertNo))
                return new EmployeeInfoEntity();
            var list = EmployeeInfoService.GetInstance().GetList(new EmployeeInfoEntity() { CertNo = newCertNo });
            if (list.Count > 0)
                return list[0];
            else
                return new EmployeeInfoEntity();
        }
    }
}
