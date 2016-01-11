
 /***************************************************************************
 *       功能：     HRContract持久层
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     用于记录合同
   （合同内工资类型等都过滤暂时不考虑）
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.Domain.Repositories;
using Project.Model.HRManager;

namespace Project.Repository.HRManager
{   
    /// <summary>
    /// 持久层
    /// </summary>
    public class  ContractRepository : RepositoryBaseSql< ContractEntity, int>
    {

    }
}




    
 

