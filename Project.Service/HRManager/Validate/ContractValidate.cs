using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model.HRManager;
using Project.Model.PermissionManager;
using Project.Repository.HRManager;
using Project.Service.PermissionManager;

namespace Project.Service.HRManager.Validate
{
    public class ContractValidate
    {
        #region
        private static readonly ContractValidate Instance = new ContractValidate();
        private readonly ContractRepository _ContractRepository;

        private ContractValidate()
        {
            _ContractRepository = new ContractRepository();
        }

        public static ContractValidate GetInstance()
        {
            return Instance;
        }

        #endregion
        public Tuple<bool, string> IsHasSameContractCode(string newContractCode, int pkId = 0)
        {
            var list = ContractService.GetInstance().GetList(new ContractEntity() { ContractNo = newContractCode });
            if (pkId > 0 && list.Any(p => p.PkId != pkId))
            {
                return new Tuple<bool, string>(false, "存在重复的合同编码！");
            }

            if (pkId <= 0 && list.Any())
            {
                return new Tuple<bool, string>(false, "存在重复的合同编码！");
            }

            return new Tuple<bool, string>(true, "存在重复的合同编码！");
        }
    }
}
