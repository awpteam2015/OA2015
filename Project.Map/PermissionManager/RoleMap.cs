
 /***************************************************************************
 *       功能：     PMRole映射类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     EC系统角色列表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;

namespace  Project.Map.PermissionManager
{
    public class RoleMap : BaseMap<RoleEntity,int>
    {
        public RoleMap():base("PM_Role")
        {
            this.MapPkidDefault<RoleEntity,int>();
 
            Map(p => p.RoleName);    
            Map(p => p.Remark);    
        }
    }
}

    
 

