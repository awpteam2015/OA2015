
 /***************************************************************************
 *       功能：     PMUserDepartment映射类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     用户所属部门
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;

namespace  Project.Map.PermissionManager
{
    public class UserDepartmentMap : BaseMap<UserDepartmentEntity,int>
    {
        public UserDepartmentMap():base("PM_UserDepartment")
        {
            this.MapPkidDefault<UserDepartmentEntity,int>();
 
            Map(p => p.UserCode);    
            Map(p => p.DepartmentCode);    
        }
    }
}

    
 

