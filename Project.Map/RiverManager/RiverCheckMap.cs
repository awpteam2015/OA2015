
 /***************************************************************************
 *       功能：     RMRiverCheck映射类
 *       作者：     李伟伟
 *       日期：     2016/7/23
 *       描述：     巡查信息
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;
using Project.Model.RiverManager;

namespace  Project.Map.RiverManager
{
    public class RiverCheckMap : BaseMap<RiverCheckEntity,int>
    {
        public RiverCheckMap():base("RM_RiverCheck")
        {
            this.MapPkidDefault<RiverCheckEntity,int>();
 
            Map(p => p.RiverId);    
            Map(p => p.RiverName);    
            Map(p => p.UserName);    
            Map(p => p.UserCode);    
            Map(p => p.Coords);    
            Map(p => p.IsActive);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreationTime);    
            Map(p => p.LastModifierUserCode);    
            Map(p => p.LastModificationTime);    
            Map(p => p.Remark);    
        }
    }
}

    
 

