
 /***************************************************************************
 *       功能：     PMFunctionDetail映射类
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     模块功能点
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;

namespace  Project.Map.PermissionManager
{
    public class FunctionDetailMap : BaseMap<FunctionDetailEntity,int>
    {
        public FunctionDetailMap():base("PM_FunctionDetail")
        {
            this.MapPkidDefault<FunctionDetailEntity,int>();
 
            Map(p => p.FunctionDetailName);    
            Map(p => p.FunctionDetailCode);    
            Map(p => p.FunctionId);    
            Map(p => p.Area);    
            Map(p => p.Controller);    
            Map(p => p.Action);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreationTime);    
            Map(p => p.LastModifierUserCode);    
            Map(p => p.LastModificationTime);    
        }
    }
}

    
 

