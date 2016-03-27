
 /***************************************************************************
 *       功能：     PMRoleDepartment映射类
 *       作者：     Roy
 *       日期：     2016/3/27
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;
using Project.Model.PermissionManager;

namespace  Project.Map.PermissionManager
{
    public class RoleDepartmentMap : BaseMap<RoleDepartmentEntity,int>
    {
        public RoleDepartmentMap():base("PM_RoleDepartment")
        {
            this.MapPkidDefault<RoleDepartmentEntity,int>();
 
            Map(p => p.RoleId);    
            Map(p => p.DepartId);    
        }
    }
}

    
 

