
 /***************************************************************************
 *       功能：     PMRoleFunctionDetail映射类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     角色对应的权限
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;

namespace  Project.Map.PermissionManager
{
    public class RoleFunctionDetailMap : BaseMap<RoleFunctionDetailEntity,int>
    {
        public RoleFunctionDetailMap():base("PM_RoleFunctionDetail")
        {
            this.MapPkidDefault<RoleFunctionDetailEntity,int>();
 
            Map(p => p.RoleId);    
            Map(p => p.FunctionId);    
            Map(p => p.FunctionDetailId);    
        }
    }
}

    
 

