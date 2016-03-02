
 /***************************************************************************
 *       功能：     PMUserFunctionDetail映射类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     用户对应的权限
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;

namespace  Project.Map.PermissionManager
{
    public class UserFunctionDetailMap : BaseMap<UserFunctionDetailEntity,int>
    {
        public UserFunctionDetailMap():base("PM_UserFunctionDetail")
        {
            this.MapPkidDefault<UserFunctionDetailEntity,int>();
 
            Map(p => p.UserCode);    
            Map(p => p.FunctionId);    
            Map(p => p.FunctionDetailId);
            Map(p => p.State);    
        }
    }
}

    
 

