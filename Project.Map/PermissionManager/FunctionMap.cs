
 /***************************************************************************
 *       功能：     PMFunction映射类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     模块功能
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;

namespace  Project.Map.PermissionManager
{
    public class FunctionMap : BaseMap<FunctionEntity,int>
    {
        public FunctionMap():base("PM_Function")
        {
            this.MapPkidDefault<FunctionEntity,int>();
 
            Map(p => p.FunctionnName);    
            Map(p => p.ModuleId);    
            Map(p => p.FunctionUrl);    
            Map(p => p.Area);    
            Map(p => p.Controller);    
            Map(p => p.Action);    
            Map(p => p.IsDisplayOnMenu);    
            Map(p => p.RankId);    
            Map(p => p.Remark);    
        }
    }
}

    
 

