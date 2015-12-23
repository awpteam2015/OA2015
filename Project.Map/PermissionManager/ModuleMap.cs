
 /***************************************************************************
 *       功能：     PMModule映射类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     权限模块
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;

namespace  Project.Map.PermissionManager
{
    public class ModuleMap : BaseMap<ModuleEntity,int>
    {
        public ModuleMap():base("PM_Module")
        {
            this.MapPkidDefault<ModuleEntity,int>();
 
            Map(p => p.ModuleName);    
            Map(p => p.ParentId);    
            Map(p => p.ModuleLevel);    
            Map(p => p.RankId);    
            Map(p => p.Remark);    
        }
    }
}

    
 

