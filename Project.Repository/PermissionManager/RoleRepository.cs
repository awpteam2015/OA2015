
 /***************************************************************************
 *       功能：     PMRole持久层
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     EC系统角色列表
 * *************************************************************************/

using NHibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.Domain.Repositories;
using Project.Model.PermissionManager;

namespace Project.Repository.PermissionManager
{   
    /// <summary>
    /// 持久层
    /// </summary>
    public class  RoleRepository : RepositoryBaseSql< RoleEntity, int>
    {
        /// <summary>
        /// 获取退款订单号编码
        /// </summary>
        /// <returns></returns>
        public string GetNewThNo()
        {
            var list = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(
                         @" select max() as value from dual ").AddScalar("value", NHibernateUtil.String);
            return list.UniqueResult().ToString();
        }

    }
}




    
 

