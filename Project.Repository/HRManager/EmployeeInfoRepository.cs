
 /***************************************************************************
 *       功能：     HREmployeeInfo持久层
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     通过FSate字段进行过滤是还是历史记录
   人员基础信息，如需要增加多字段请使用扩展表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.Domain.Repositories;
using Project.Model.HRManager;

namespace Project.Repository.HRManager
{   
    /// <summary>
    /// 持久层
    /// </summary>
    public class  EmployeeInfoRepository : RepositoryBaseSql<EmployeeInfoEntity, int>
    {

    }
}




    
 

