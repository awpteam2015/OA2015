
 /***************************************************************************
 *       功能：     PMUserRole映射类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     EC系统用户和角色对应关系表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;

namespace  Project.Map.PermissionManager
{
    public class UserRoleMap : BaseMap<UserRoleEntity,int>
    {
        public UserRoleMap():base("PM_UserRole")
        {
            this.MapPkidDefault<UserRoleEntity,int>();
 
            Map(p => p.UserCode);    
            Map(p => p.RoleId);    
        }
    }
}

    
 

