
 /***************************************************************************
 *       功能：     RMRiverOwer映射类
 *       作者：     李伟伟
 *       日期：     2016/7/24
 *       描述：     河道河长绑定关系
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.RiverManager;

namespace  Project.Map.RiverManager
{
    public class RiverOwerMap : BaseMap<RiverOwerEntity,int>
    {
        public RiverOwerMap():base("RM_RiverOwer")
        {
            this.MapPkidDefault<RiverOwerEntity,int>();
 
            Map(p => p.RiverId);    
            Map(p => p.RiverName);    
            Map(p => p.UserCode);    
            Map(p => p.UserName);    
        }
    }
}

    
 

